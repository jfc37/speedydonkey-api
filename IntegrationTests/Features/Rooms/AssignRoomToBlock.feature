Feature: AssignRoomToBlock

@room @block @assign_room @golden_path
Scenario: Assign block to class
	Given a block exists
	And a room exists
	And the block needs to be assigned a room
	When the block room assignment is requested
	Then the request is successful
	And the block details has the room
	And all the classes in the block has the room
	And the room has the classes in its schedule

@room @block @assign_room @validation_error
Scenario: Try to double book a room
	Given '2' blocks exists
	And a room exists
	And the block is assigned to the room
	When another block at the same time needs to be assigned to the same room
	Then the request is successful
	But all the classes in the block does not have the room
	And the room does not have the blocks classes in its schedule

@room @block @create @assign_room @golden_path
Scenario: Create block with room
	Given a room exists
	And a valid block is ready to be submitted
	And the pending block is to be held in the room
	When the block is attempted to be created
	And the block details has the room
	And all the classes in the block has the room
	And the room has the classes in its schedule
