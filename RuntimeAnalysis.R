# Basic values
#Y <- c(2.4328, 3.3328, 4.1925, 5.6317, 7.2494, 10.3862, 25.8814, 60.7398, 166.744, 591.64, 1630.536, 9396.296)

# Optimized values
Y <- c(0.4304, 0.6126, 0.7462, 1.0659, 1.4456,  1.9872,  4.1472, 22.2387, 124.0676,145.086, 535.761, 5185.273)

Y <- c(19.2,24.8,31.6,34.2,40.0,48.8,55.2,80.6,142.8,228.2,553.6,1090,1866.6)

X <- seq(1, length(Y))

# Create a dataframe with X and Y
df <- data.frame(X= X, Y= Y)

# 1. Logarithmic: O(log N)
m_logarithmic <- lm(Y ~ log(X), data = df)

# 2. Linear: O(N)
m_linear <- lm(Y ~ X, data = df)

# 3. Loglinear: O(N log N)
m_loglinear <- lm(Y ~ X * log(X), data = df)

# 4. Quadratic: O(N²)
m_quadratic <- lm(Y ~ X + I(X^2), data = df)

# 5. Exponential: O(2^N)
m_exponential <- lm(Y ~ I(2^X), data = df)

# Extract R-squared values for all models
r_squared_values <- data.frame(
  Model = c("Logarithmic", "Linear", "Loglinear", "Quadratic", "Exponential"),
  R_squared = c(
    summary(m_logarithmic)$r.squared,
    summary(m_linear)$r.squared,
    summary(m_loglinear)$r.squared,
    summary(m_quadratic)$r.squared,
    summary(m_exponential)$r.squared
  )
)

# Print the R-squared values for each model
print(r_squared_values)

