# feedback-server

[//]: # ('&' - flow context)
[//]: # ('#' - model definition context)
[//]: # ('-' - action in context)

& Flow user submits feedback
- user uses js feedback widget to submit a feedback message
- feedback is saved on the server
- notification about feedback is send to the administrator

& Flow user creates feedback server account
- generate account API key and configure with domain(s)

& Flow user installs feedback client widget on his website
- user has access to js lib
- user know how to configure js lib
    - provide API key
    - styling

& User browses the provided feedback
- user can export the data as excel

	(??)
% Feedback configuration
- id
- user id
- website address

% Feedback model
- id
- feedback config id (??)
- domain id
- message
- create date