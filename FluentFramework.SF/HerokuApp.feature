Feature: HerokuApp

Scenario: Load HerokuApp
	Given I have navigated to the HerokuApp main page
	 When I click link WYSIWYG Editor
	 Then I should see URL partial /tinymce