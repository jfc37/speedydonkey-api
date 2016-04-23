Feature: BlockEnrolment

@block @enrolment
Scenario: User enrols in a block
Given a block exists
When the user enrols in the block
Then the user is enroled in the block
And the user has an item in their upcoming schedule
And the user sees the block as already enrolled
And the number of spaces available has decreased

@block @available_blocks
Scenario: User sees which blocks they can enrol in
Given a block exists
When blocks for enrolment is requested
Then the request is successful
And there are blocks available for enrolment

@block @enrolment @validation_error
Scenario: User can not enrol in a full block
Given a block exists that is full
When the user tries to enrol in the block
Then the request is unsuccessful
And validation errors are returned