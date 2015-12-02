Feature: UnassignRoomFromClass

@room @class @unassign_room @golden_path
Scenario: Unassign room from class
	Given a block exists
	And a room exists
	And a class is assigned to the room
	When the class room unassignment is requested
	Then the request is successful
	And the class details does not have the room
	And the room does not have the class in its schedule