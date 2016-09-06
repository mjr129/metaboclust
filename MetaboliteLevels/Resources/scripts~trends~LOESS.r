## y = Y
## x = X
## x.out = X.OUT
## span = integer

model = loess(y ~ x, span = span)
result = predict(model, x.out)

result