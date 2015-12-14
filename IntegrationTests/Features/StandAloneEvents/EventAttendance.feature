Feature: EventAttendance

@stand_alone_event @attendance @golden_path
Scenario: User attends an event
	Given a stand alone event exists
	And a user exists
	When the teacher checks the student into the event
	Then the request is successful
	And the student is marked against the event

@class @attendance @unattend
Scenario: User attends then unattends an event
	Given a stand alone event exists
	And a user exists
	And the teacher has checked the student into the event
	When the teacher unchecks the student into the event
	Then the student isnt marked against the event
