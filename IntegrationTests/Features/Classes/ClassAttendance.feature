Feature: ClassAttendance

@class @attendance @golden_path
Scenario: User attends a class
	Given a block exists
	And a user exists
	And the user has a valid clip pass
	And the user attends a class
	When the teacher checks the student in
	Then check in is successful
	Then the student is marked against class
	And a clip has been removed from the pass

@class @attendance @validation_errors
Scenario: User attends a class without a valid pass
	Given a block exists
	And a user exists
	And the user doesn't have a pass
	And the user attends a class
	When the teacher checks the student in
	Then validation errors are returned
	Then the student isnt marked against class

@class @attendance @unattend
Scenario: User attends then unattends a class
	Given a block exists
	And a user exists
	And the user has a valid clip pass
	And the teacher has checked the student in
	When the teacher unchecks the student in
	Then the student isnt marked against class
	And a clip has not been removed from the pass
