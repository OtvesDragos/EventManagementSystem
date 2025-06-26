A RESTful API for managing and responding to events. Built with ASP.NET Core, supports JWT Bearer authentication, and integrates with Entity Framework Core.

FEATURES
  - Login and Authentication (create user)
  - public (anyone can RSVP) and private events (authorized and invited users can RSVP)
  - authorized users can:
	- create, edit and delete events
	- add invites for private events
	- view events by code
	- view a list of their events
  - unauthorized user can
	- view public events
	- respond to public events

ENCRYPTION ALGORITHMS 
  	- argon2 for password hashing
  	- SHA256 for email hashing
  	- HmacSha256 for JWT token

GETTING STARTED
  1. Clone the repository
  2. Run DataBase script (location: .\DataAccess\DatabaseScripts\schema_dump.sql)
  3. Run the API
  4. Create a user using Authenticate endpoint
  5. Use Login endpoint to get the JWT bearer token
  6. Authenticate using SWAGGER's Authentication using "Bearer <obtained_JWT_token>"

ENDPOINTS
  	- endpoints that request authorization have a lock icon near them
	- each controller handles their DataBase table (e.g. Auth handles users, Event hadles events etc.)