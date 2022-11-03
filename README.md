# AirQuality

- API is canse sensitive, created solution which works for lower case cities like 'salt lake city'
- Created own DI registration by attributes
- created singleton http client for API
- Creation of many instances is moved to factories, which helps when writing unit tests
- ViewModel support few error types - internal server error when calling API, non-200 http statuses.
- Presentation is done in responsive way - 2 columns on large devices, 1 column on medium.
