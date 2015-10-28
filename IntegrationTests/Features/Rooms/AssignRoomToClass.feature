Feature: AssignRoomToClass

@room @class @assign_room @golden_path
Scenario: Assign room to class
	Given a block exists
	And a room exists
	And a class needs to be assigned a room
	When the class room assignment is requested
	Then the request is successful
	And the class details has the room
	And the room has the class in its schedule

@room @class @assign_room @validation_error
Scenario: Try to double book a room
	Given '2' blocks exists
	And a room exists
	And a class is assigned to the room
	When another class at the same time needs to be assigned to the same room
	Then validation errors are returned
	And the class details does not have the room
	And the room does not have the class in its schedule

