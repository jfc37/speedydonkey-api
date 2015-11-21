Feature: BlockEnrolment

@block @enrolment
Scenario: User enrols in a block
Given a block exists
When the user enrols in the block
Then the user is enroled in the block
And the user has an item in their upcoming schedule

@block @available_blocks
Scenario: User see's which blocks they can enrol in
Given a block exists
When blocks for enrolment is requested
Then the request is successful
And there are blocks available for enrolment