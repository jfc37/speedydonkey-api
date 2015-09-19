Feature: RetrieveTeacher

@teacher @retrieve @get_all
Scenario: Get all teachers
	Given an existing user is a teacher
	When all teachers are retreived
	Then something is retreived

@teacher @retrieve @search
Scenario: Search teachers
	Given an existing user is a teacher
	When a teacher search is performed
	Then something is retreived

@teacher @retrieve @get_by_id
Scenario: Get teacher by id
	Given an existing user is a teacher
	When a teacher is retrieved by id
	Then something is retreived