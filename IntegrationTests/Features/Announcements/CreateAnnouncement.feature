Feature: CreateAnnouncement

@announcement @create @golden_path
Scenario: Send email to single block
	Given a block exists
	And a valid announcement is ready to be submitted
	And the announcement is to be sent to the block
	When the announcement is attempted to be created
	Then announcement request is successful

@announcement @create @golden_path
Scenario: Send email to two blocks
	Given a block exists
	And a block exists
	And a valid announcement is ready to be submitted
	And the announcement is to be sent to multiple blocks
	When the announcement is attempted to be created
	Then announcement request is successful

@announcement @create @golden_path
Scenario: Send email to all users
	Given a valid announcement is ready to be submitted
	And the announcement is to be sent to all users
	When the announcement is attempted to be created
	Then announcement request is successful

@announcement @create @validation_error
Scenario: Try to send email to no one
	Given a valid announcement is ready to be submitted
	And the announcement is to be sent to no one
	When the announcement is attempted to be created
	Then announcement request fails

@announcement @create @validation_error
Scenario: Try to send email with no message
	Given a valid announcement is ready to be submitted
	And the announcement is missing the message
	When the announcement is attempted to be created
	Then announcement request fails

@announcement @create @validation_error
Scenario: Try to send email with no subject
	Given a valid announcement is ready to be submitted
	And the announcement is missing the subject
	When the announcement is attempted to be created
	Then announcement request fails
