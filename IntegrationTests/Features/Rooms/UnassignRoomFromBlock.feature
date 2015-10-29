Feature: UnassignRoomFromBlock

@room @block @unassign_room @golden_path
Scenario: Unassign block from class
	Given a block exists
	And a room exists
	And the block is assigned to the room
	When the block room unassignment is requested
	Then the request is successful
	And the block details does not have the room
	And all the classes in the block does not have the room
	And the room does not have the blocks classes in its schedule