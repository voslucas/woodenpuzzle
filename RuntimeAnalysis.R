# Basic values
Y <- c(4.84,6.74,8.63,11.48,14.60,18.21,23.23,34.14,61.68,122.41,326.10,1670.59)


# Optimized values
Y <- c(0.65,0.88,1.11,1.55,1.99,2.65,4.49,12.16,36.00,88.91,278.42,1822.28)

# Imperative values
#Y <- c(19.2,24.8,31.6,34.2,40.0,48.8,55.2,80.6,142.8,228.2,553.6,1090,1866.6)

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

