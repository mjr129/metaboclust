﻿using MetaboliteLevels.Algorithms;
using MetaboliteLevels.Algorithms.Statistics;
using MetaboliteLevels.Controls;
using MetaboliteLevels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaboliteLevels.Algorithms.Statistics.Configurations;
using MetaboliteLevels.Data.Visualisables;
using MetaboliteLevels.Utilities;
using System.Collections.ObjectModel;
using MetaboliteLevels.Data.DataInfo;

namespace MetaboliteLevels.Settings
{
    /// <summary>
    /// Filters (Peak and Observations)
    /// 
    /// IMMUTABLE (exc. names, comments and enabled status)
    /// </summary>
    [Serializable]
    internal abstract class Filter
    {
        /// <summary>
        /// Name, provided by user
        /// Doesn't affect the filter itself
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Comments, provided by user
        /// Doesn't affect the filter itself
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Enabled or disabled (list visibility), provided by user
        /// Doesn't affect the filter itself
        /// </summary>
        public bool Enabled { get; set; }

        public abstract string ParamsAsString();

        public enum EStatOperator
        {
            [Name("<")]
            LessThan,

            [Name("≤")]
            LessThanEq,

            [Name(">")]
            MoreThan,

            [Name("≥")]
            MoreThanEq,
        }

        [Flags]
        public enum ESetOperator
        {
            [Name("_Not")]
            Negate = 1,

            [Name("any in")]
            AnyXinY = 2,

            [Name("all in")]
            AllXInY = 4,

            [Name("contains any")]
            AnyYInX = 8,

            [Name("contains all")]
            AllYInX = 16,

            [Name("none in")]
            NotAnyXinY = AnyXinY | Negate,

            [Name("not all in")]
            NotAllXInY = AllXInY | Negate,

            [Name("does not contain any")]
            NotAnyYInX = AnyYInX | Negate,

            [Name("does not contain all")]
            NotAllYInX = AllYInX | Negate,

            [Name("_BasicMask")]
            BasicMask = AnyXinY | AllXInY | AnyYInX | AllYInX,
        }

        public enum ELogicOperator
        {
            [Name("and")]
            And,

            [Name("or")]
            Or,
        }

        public enum EElementOperator
        {
            [Name("is")]
            Is,

            [Name("is not")]
            IsNot,
        }

        protected Filter(string name, string comments)
        {
            this.Name = name;
            this.Comments = comments;
            this.Enabled = true;
        }
    }

    /// <summary>
    /// Filters (Peak and Observations)
    /// 
    /// IMMUTABLE (exc. names, comments and enabled status)
    /// </summary>
    /// <typeparam name="T">Type to filter (currently Peak or ObservationInfo)</typeparam>
    [Serializable]
    internal abstract class Filter<T> : Filter
    {
        /// <summary>
        /// Conditions of the filter.
        /// </summary>
        public readonly ReadOnlyCollection<ConditionBase> Conditions;

        /// <summary>
        /// Ctor
        /// </summary>
        protected Filter(string name, string comments, IEnumerable<ConditionBase> filters)
            : base(name, comments)
        {
            if (filters != null)
            {
                Conditions = new ReadOnlyCollection<ConditionBase>(filters.ToList());
            }
            else
            {
                Conditions = new ReadOnlyCollection<ConditionBase>(new List<ConditionBase>());
            }
        }

        /// <summary>
        /// Base class for condition
        /// </summary>
        [Serializable]
        public abstract class ConditionBase
        {
            public readonly ELogicOperator CombiningOperator;
            public readonly bool Negate;

            protected ConditionBase(ELogicOperator op, bool negate)
            {
                this.Negate = negate;
                this.CombiningOperator = op;
            }

            public bool Preview(T target)
            {
                return (Test(target) ^ Negate);
            }

            public bool Test(bool previousResult, T target)
            {
                switch (CombiningOperator)
                {
                    case ELogicOperator.And:
                        return previousResult && (Test(target) ^ Negate);

                    case ELogicOperator.Or:
                        return previousResult || (Test(target) ^ Negate);

                    default:
                        throw new SwitchException(CombiningOperator);
                }
            }

            protected abstract bool Test(T target);
        }

        /// <summary>
        /// Returns the conditions of the filter as a string (non reversable).
        /// </summary>
        public sealed override string ParamsAsString()
        {
            if (IsEmpty)
            {
                return "No filter";
            }

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < Conditions.Count; index++)
            {
                var test = Conditions[index];

                if (index != 0)
                {
                    switch (test.CombiningOperator)
                    {
                        case ELogicOperator.And:
                            sb.Append(" ⋏ ");
                            break;

                        case ELogicOperator.Or:
                            sb.Append(" ⋎ ");
                            break;
                    }
                }

                if (test.Negate)
                {
                    sb.Append("¬");
                }

                if (Conditions.Count != 1)
                {
                    sb.Append("(");
                }

                sb.Append(test.ToString());

                if (Conditions.Count != 1)
                {
                    sb.Append(")");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Describes the filter (Name ?? ParamsAsString()).
        /// </summary>
        public override string ToString()
        {
            return Name ?? ParamsAsString();
        }

        /// <summary>
        /// Returns if the filter allows everything through.
        /// </summary>
        internal bool IsEmpty
        {
            get
            {
                return Conditions.Count == 0;
            }
        }

        /// <summary>
        /// Results of testing a list.
        /// 
        /// IMMUTABLE
        /// </summary>
        public class Results
        {
            public readonly ReadOnlyCollection<T> Passed;
            public readonly ReadOnlyCollection<T> Failed;

            public Results(ReadOnlyCollection<T> Passed, ReadOnlyCollection<T> Failed)
            {
                this.Passed = Passed;
                this.Failed = Failed;
            }
        }

        /// <summary>
        /// Tests a target against the filter.
        /// </summary>
        public bool Test(T target)
        {
            bool test = true;

            foreach (ConditionBase t in this.Conditions)
            {
                test = t.Test(test, target);
            }

            return test;
        }

        /// <summary>
        /// Tests a set of targets against the filter.
        /// </summary>
        public Results Test(IEnumerable<T> targets)
        {
            List<T> passed = new List<T>();
            List<T> failed = new List<T>();

            foreach (T p in targets)
            {
                if (Test(p))
                {
                    passed.Add(p);
                }
                else
                {
                    failed.Add(p);
                }
            }

            return new Results(passed.AsReadOnly(), failed.AsReadOnly());
        }

        protected static bool CompareSets<U>(ESetOperator op, IEnumerable<U> enumerable, IEnumerable<U> compareTo)
        {
            bool negate = op.HasFlag(ESetOperator.Negate);

            switch (op & ESetOperator.BasicMask)
            {
                case ESetOperator.AllXInY:
                    return compareTo.All(enumerable.Contains) ^ negate;

                case ESetOperator.AnyXinY:
                    return compareTo.Any(enumerable.Contains) ^ negate;

                case ESetOperator.AllYInX:
                    return enumerable.All(compareTo.Contains) ^ negate;

                case ESetOperator.AnyYInX:
                    return enumerable.Any(compareTo.Contains) ^ negate;

                default:
                    throw new SwitchException(op);
            }
        }

        protected static bool CompareElement<U>(EElementOperator op, U p, IEnumerable<U> compareTo)
        {
            switch (op)
            {
                case EElementOperator.Is:
                    return compareTo.Contains(p);

                case EElementOperator.IsNot:
                    return !compareTo.Contains(p);

                default:
                    throw new SwitchException(op);
            }
        }
    }

    [Serializable]
    internal class ObsFilter : Filter<ObservationInfo>
    {
        public static ObsFilter Empty = new ObsFilter(null, null, null);

        public ObsFilter(string name, string comments, IEnumerable<Condition> filters)
            : base(name, comments, filters)
        {
            // NA
        }

        [Serializable]
        public abstract class Condition : ConditionBase
        {
            public Condition(ELogicOperator op, bool negate)
                : base(op, negate)
            {
                // NA
            }
        }

        [Serializable]
        public sealed class ConditionGroup : Condition
        {
            public readonly ReadOnlyCollection<GroupInfo> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionGroup(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<GroupInfo> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci.Group, Possibilities);
            }

            public override string ToString()
            {
                return "Gʀᴏᴜᴘ " + Operator.ToUiString() + " {" + StringHelper.ArrayToString(Possibilities, z => z.ShortName) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionRep : Condition
        {
            public readonly ReadOnlyCollection<int> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionRep(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<int> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci.Rep, Possibilities);
            }

            public override string ToString()
            {
                return "Rᴇᴘʟɪᴄᴀᴛᴇ " + Operator.ToUiString() + " {" + StringHelper.ArrayToStringInt(Possibilities) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionBatch : Condition
        {
            public readonly ReadOnlyCollection<BatchInfo> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionBatch(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<BatchInfo> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci.Batch, Possibilities);
            }

            public override string ToString()
            {
                return "Bᴀᴛᴄʜ " + Operator.ToUiString() + " {" + StringHelper.ArrayToString(Possibilities, z => z.Id.ToString()) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionAcquisition : Condition
        {
            public readonly ReadOnlyCollection<int> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionAcquisition(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<int> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci.Acquisition, Possibilities);
            }

            // ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘʀꜱᴛᴜᴠᴡʏᴢ
            public override string ToString()
            {
                // Oʀᴅᴇʀ
                return "Aᴄǫᴜɪsɪᴛɪᴏɴ " + Operator.ToUiString() + " {" + StringHelper.ArrayToStringInt(Possibilities) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionTime : Condition
        {
            public readonly ReadOnlyCollection<int> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionTime(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<int> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci.Time, Possibilities);
            }

            public override string ToString()
            {
                return "Tɪᴍᴇ " + Operator.ToUiString() + " {" + StringHelper.ArrayToStringInt(Possibilities) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionObservation : Condition
        {
            public readonly ReadOnlyCollection<ObservationInfo> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionObservation(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<ObservationInfo> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci, Possibilities);
            }

            public override string ToString()
            {
                return "Oʙsᴇʀᴠᴀᴛɪᴏɴ " + Operator.ToUiString() + " {" + StringHelper.ArrayToString(Possibilities) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionCondition : Condition
        {
            public readonly ReadOnlyCollection<ConditionInfo> Possibilities;
            public readonly EElementOperator Operator;

            public ConditionCondition(ELogicOperator lop, bool negate, EElementOperator op, IEnumerable<ConditionInfo> enu)
                : base(lop, negate)
            {
                Operator = op;
                Possibilities = enu.ToList().AsReadOnly();
            }

            protected override bool Test(ObservationInfo ci)
            {
                return CompareElement(Operator, ci._conditions, Possibilities);
            }

            public override string ToString()
            {
                return "Cᴏɴᴅɪᴛɪᴏɴ " + Operator.ToUiString() + " {" + StringHelper.ArrayToString(Possibilities) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionFilter : Condition
        {
            public readonly bool FilterOp;
            public readonly ObsFilter Filter;

            public ConditionFilter(ELogicOperator op, bool negate, ObsFilter filter, bool filterOp)
                : base(op, negate)
            {
                this.Filter = filter;
                this.FilterOp = filterOp;
            }

            protected override bool Test(ObservationInfo p)
            {
                return Filter.Test(p) == FilterOp;
            }

            public override string ToString()
            {
                return "Fɪʟᴛᴇʀ {" + Filter.ToString() + "} = " + FilterOp;
            }
        }

        internal bool Test(ConditionInfo arg)
        {
            return base.Test(new ObservationInfo(arg, 0, null, 0));
        }

        internal IEnumerable<ConditionInfo> Test(IEnumerable<ConditionInfo> args)
        {
            foreach (var arg in args)
            {
                if (base.Test(new ObservationInfo(arg, 0, null, 0)))
                {
                    yield return arg;
                }
            }
        }
    }

    /// <summary>
    /// Significance filter.
    /// 
    /// IMMUTABLE (exc. names and comments)
    /// </summary>
    [Serializable]
    class PeakFilter : Filter<Peak>
    {
        public static readonly PeakFilter Empty = new PeakFilter(null, null, null);

        public PeakFilter(string name, string comments, IEnumerable<Condition> filters)
            : base(name, comments, filters)
        {
            // NA
        }

        [Serializable]
        public abstract class Condition : ConditionBase
        {
            protected Condition(ELogicOperator op, bool negate)
                : base(op, negate)
            {
                // NA
            }
        }

        [Serializable]
        public sealed class ConditionStatistic : Condition
        {
            public readonly EStatOperator StatisticOp;
            public readonly WeakReference<ConfigurationStatistic> Statistic;
            public readonly double StatisticValue;

            public ConditionStatistic(ELogicOperator op, bool negate, EStatOperator statOp, ConfigurationStatistic stat, double statisticValue)
                : base(op, negate)
            {
                this.StatisticOp = statOp;
                this.StatisticValue = statisticValue;
                this.Statistic = new WeakReference<ConfigurationStatistic>(stat);
            }

            protected override bool Test(Peak p)
            {
                double v;

                ConfigurationStatistic stat = Statistic.GetTarget();

                if (stat == null)
                {
                    throw new InvalidOperationException("Statistic required for significance detection but statistic is no longer available.");
                }

                if (!p.Statistics.TryGetValue(stat, out v))
                {
                    throw new InvalidOperationException("Statistic required (" + Statistic + ") for significance detection but statistic is no longer available.");
                }

                switch (StatisticOp)
                {
                    case EStatOperator.LessThan:
                        return v < StatisticValue;

                    case EStatOperator.LessThanEq:
                        return v <= StatisticValue;

                    case EStatOperator.MoreThan:
                        return v > StatisticValue;

                    case EStatOperator.MoreThanEq:
                        return v >= StatisticValue;

                    default:
                        throw new InvalidOperationException("Invalid switch: " + StatisticOp);
                }
            }

            public override string ToString()
            {
                return Statistic.SafeToString() + " " + StatisticOp.ToUiString() + " " + StatisticValue;
            }
        }

        [Serializable]
        public sealed class ConditionFilter : Condition
        {
            public readonly bool FilterOp;
            public readonly PeakFilter Filter;

            public ConditionFilter(ELogicOperator op, bool negate, PeakFilter filter, bool filterOp)
                : base(op, negate)
            {
                this.Filter = filter;
                this.FilterOp = filterOp;
            }

            protected override bool Test(Peak p)
            {
                return Filter.Test(p) == FilterOp;
            }

            // ᴀʙᴄᴅᴇꜰɢʜɪᴊᴋʟᴍɴᴏᴘʀꜱᴛᴜᴠᴡʏᴢ
            public override string ToString()
            {
                return "Fɪʟᴛᴇʀ {" + Filter.ToString() + "} = " + FilterOp;
            }
        }

        [Serializable]
        public sealed class ConditionPeak : Condition
        {
            public readonly EElementOperator PeaksOp;
            public readonly ReadOnlyCollection<WeakReference<Peak>> Peaks;

            public ConditionPeak(ELogicOperator op, bool negate, IEnumerable<Peak> peaks, EElementOperator peaksOp)
                : base(op, negate)
            {
                this.Peaks = new ReadOnlyCollection<WeakReference<Peak>>(peaks.Select(WeakReferenceHelper.ToWeakReference).ToList());
                this.PeaksOp = peaksOp;
            }

            protected override bool Test(Peak p)
            {
                return CompareElement(PeaksOp, p, Peaks.Select(z => z.GetTargetOrThrow()));
            }

            public override string ToString()
            {
                return "Pᴇᴀᴋ " + PeaksOp.ToUiString() + " {" + StringHelper.ArrayToString(Peaks, z => z.DisplayName) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionCluster : Condition
        {
            public readonly ESetOperator ClustersOp;
            public readonly ReadOnlyCollection<WeakReference<Cluster>> Clusters;

            public ConditionCluster(ELogicOperator op, bool negate, ESetOperator clusterOp, IEnumerable<Cluster> clusters)
                : base(op, negate)
            {
                this.ClustersOp = clusterOp;
                this.Clusters = new ReadOnlyCollection<WeakReference<Cluster>>(clusters.Select(WeakReferenceHelper.ToWeakReference).ToList());
            }

            protected override bool Test(Peak p)
            {
                return CompareSets(ClustersOp, p.Assignments.Clusters, Clusters.Select(z => z.GetTargetOrThrow()));
            }

            public override string ToString()
            {
                return "Pᴀᴛᴛᴇʀɴ " + ClustersOp.ToUiString() + " {" + StringHelper.ArrayToString(Clusters, z => z.DisplayName) + "}";
            }
        }

        [Serializable]
        public sealed class ConditionFlags : Condition
        {
            public readonly ESetOperator FlagsOp;
            public readonly ReadOnlyCollection<WeakReference<PeakFlag>> Flags;

            public ConditionFlags(ELogicOperator op, bool negate, ESetOperator flagsOp, IEnumerable<PeakFlag> flags)
                : base(op, negate)
            {
                this.FlagsOp = flagsOp;
                this.Flags = new ReadOnlyCollection<WeakReference<PeakFlag>>(flags.Select(WeakReferenceHelper.ToWeakReference).ToList());
            }

            protected override bool Test(Peak p)
            {
                return CompareSets(FlagsOp, p.CommentFlags, Flags.Select(z => z.GetTargetOrThrow()));
            }

            public override string ToString()
            {
                return "Fʟᴀɢ " + FlagsOp.ToUiString() + " {" + StringHelper.ArrayToString(Flags, z => z.Id) + "}";
            }
        }
    }
}

