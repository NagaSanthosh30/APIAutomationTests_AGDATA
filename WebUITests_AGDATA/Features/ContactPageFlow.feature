Feature: ContactPageFlow

A short summary of the feature

@FeatureTesting
@high
Scenario: Validate Contact Page
	Given User Navigating to MarketIntelligence
	When Follwing headings should be present 'Minimize Costs','Generate Revenue','Mitigate Risk'
	And Click on LetsGetStarted
	Then Contact Page should be Displayed
