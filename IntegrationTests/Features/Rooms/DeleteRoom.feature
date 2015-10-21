Feature: DeleteRoom

@room @delete @golden_path
Scenario: Delete room
	Given a room exists
	When the room is deleted
	Then the request is successful
	And the room can not be retrieved

