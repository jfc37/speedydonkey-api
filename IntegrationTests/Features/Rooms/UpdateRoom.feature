Feature: UpdateRoom

@room @update @golden_path
Scenario: Update a room
	Given a room exists
	And the room needs to be changed
	When the room is updated
	Then the request is successful
	Then the room can be retrieved

@room @update @validation_error
Scenario: Try to update an invalid room
	Given a room exists
	And the room needs to be changed
	When the room is attempted to be created
	Then validation errors are returned
	And the room can not be retrieved
