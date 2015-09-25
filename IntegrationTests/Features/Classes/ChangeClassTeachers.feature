Feature: ChangeClassTeachers

@class @update @golden_path @change_teacher
Scenario: Change class teachers
	Given a block exists
	And an existing user is a teacher
	And an existing user is a teacher
	And a class needs the teachers changed
	When the class teachers are changed
	Then the class teachers are updated