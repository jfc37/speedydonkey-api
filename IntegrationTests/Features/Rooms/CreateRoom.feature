Feature: CreateRoom

@room @create @golden_path
Scenario: Create a room
	Given a valid room is ready to be submitted
	When the room is attempted to be created
	Then the room can be retrieved

@room @create @validation_error
Scenario: Try to set up an invalid room
	Given an invalid room is ready to be submitted
	When the room is attempted to be created
	Then validation errors are returned
	And the room can not be retrieved
