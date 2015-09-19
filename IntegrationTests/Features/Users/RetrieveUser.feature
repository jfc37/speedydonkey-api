Feature: RetrieveUser

@user @retrieve @get_all
Scenario: Get all users
	Given a user exists
	When all users are retreived
	Then something is retreived

@user @retrieve @search
Scenario: Search users
	Given a user exists
	When a user search is performed
	Then something is retreived

@user @retrieve @search
Scenario: Get user by id
	Given a user exists
	When a user is retrieved by id
	Then something is retreived

