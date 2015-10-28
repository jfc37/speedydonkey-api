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
	But the room has invalid details
	When the room is updated
	Then validation errors are returned
	And the request is unsuccessful
