Feature: GetClaims
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@claims @user @get
Scenario: Get user claims
	Given a user exists
	When their claims are retrieved
	Then their claims are empty

@claims @teacher @get
Scenario: Get teacher claims
	Given an existing user is a teacher
	When their claims are retrieved
	Then their claims are 'teacher'

@claims @admin @get
Scenario: Get admin claims
	Given an admin user
	When their claims are retrieved
	Then their claims are 'teacher,admin'

