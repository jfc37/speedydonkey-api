Feature: TeacherRates

@teacher @teacher_rates @set_rate @golden_path
Scenario: Set rate for a teacher
	Given an existing user is a teacher
	When the rate for the teacher is changed
	Then the request is successful
	And the rate for the teacher is updated

@teacher @teacher_rates @get_rates @golden_path
Scenario: Get rates for all teachers
	Given '2' teachers exist
	When the rates for all teachers are requested
	Then the request is successful
	And the rates for '2' teachers are retrieved