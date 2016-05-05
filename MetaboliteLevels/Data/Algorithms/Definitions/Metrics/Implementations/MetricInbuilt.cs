﻿using System;
using MetaboliteLevels.Algorithms.Statistics.Arguments;
using MetaboliteLevels.Algorithms.Statistics.Inputs;

namespace MetaboliteLevels.Algorithms.Statistics.Metrics
{
    /// <summary>
    /// Inbuilt metrics.
    /// </summary>
    class MetricInbuilt : MetricBase
    {
        public readonly AlgoDelegate_Input2 _delegate;

        public MetricInbuilt(AlgoDelegate_Input2 method, bool isMathDotNet)
            : base(method.Method.Name.ToUpper(), method.Method.Name)
        {
            this._delegate = method;
            this.IsMathDotNet = isMathDotNet;
        }

        public MetricInbuilt(AlgoDelegate_Input2 method, string id, string name, bool isMathDotNet )
            : base(id, name)
        {
            this._delegate = method;
            this.IsMathDotNet = isMathDotNet;
        }

        public override double Calculate(InputStatistic input)
        {
            double[] a = input.GetData(EAlgoInput.A, true, false, false, false, false).Primary;
            double[] b = input.GetData(EAlgoInput.B, true, false, false, false, false).Primary;

            return _delegate(a, b);
        }

        public override double QuickCalculate(double[] a, double[] b, object[] parameters)
        {
            return _delegate(a, b);
        }

        protected override AlgoParameterCollection CreateParamaterDesription()
        {
            return new AlgoParameterCollection();
        }

        public override bool SupportsQuickCalculate
        {
            get
            {
                return true;
            }
        }

        public override bool SupportsInputFilters { get { return true; } }
    }
}