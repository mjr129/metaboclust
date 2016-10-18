## y = Y
## x = X
## x.out = X.OUT
## window = integer = Window width

# There is already a moving mean built into the application
# This demonstrates the functionality in R

window = (window - 1) / 2
result = rep(NA, length(x.out))
    
for(o in seq_along(x.out))
{
    i = x.out[o]
    w = (x.out >= (i - window)) & (x.out <= (i + window))
    src = y[w]
    result[o] = mean(src)
}
    
result