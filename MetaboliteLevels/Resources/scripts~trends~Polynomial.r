## y = Y
## x = X
## x.out = X.OUT
## degree = double = Degree of the polynomial

model = lm(y ~ poly(x, degree, raw = TRUE))
    
result = predict(model, newdata = data.frame(x = x.out))

result