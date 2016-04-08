Feature: SetTeacherAvailability
#
#@teacher_availability @create @golden_path
#Scenario: Set teacher availability
#	Given the current user is a teacher
#	And a valid teacher availability is ready to be submitted
#	When the teacher availability is attempted to be created
#	Then the request is successful
#	And the teacher availability can be retrieved
#
#
#@teacher_availability @create @validation_errors
#Scenario: Set invalid teacher availability
#	Given a invalid teacher availability is ready to be submitted
#	When the teacher availability is attempted to be created
#	Then the request is unsuccessful
#	And the teacher availability cannot be retrieved
#
#@teacher_availability @update @golden_path
#Scenario: Update opening hours
#	Given teacher availability are set
#	And teacher availability need to be changed
#	When the teacher availability is attempted to be updated
#	Then the request is successful
#	And the teacher availability can be retrieved