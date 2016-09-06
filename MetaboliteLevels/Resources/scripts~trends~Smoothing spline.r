## y = Y
## x = X
## x.out = X.OUT

model = smooth.spline(x, y)
result = predict(model, x.out)$y

result