Feature: SetRatesForATeacher

@teacher @set_rate @golden_path
Scenario: Set rate for a teacher
	Given an existing user is a teacher
	When the rate for the teacher is changed
	Then the request is successful
	And the rate for the teacher is updated