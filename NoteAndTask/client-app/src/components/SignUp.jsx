import React from "react";
import { AuthLayout } from "./layout/AuthLayout";
import {
  Button,
  Content,
  ControlLabel,
  FlexboxGrid,
  Form,
  FormControl,
  FormGroup,
  Panel
} from "rsuite";

export class SignUp extends React.Component {
  static displayName = SignUp.name;

  handleSubmit = async event => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      let formData = {
        name: event.target.name.value,
        email: event.target.email.value,
        phoneNumber: event.target.phoneNumber.value,
        password: event.target.password.value,
        passwordCompare: event.target.passwordCompare.value
      };

      await fetch("api/auth/signup", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData)
      })
        .then(response => response.json())
        .then(data => {
          if (data) {
            localStorage.setItem("token", data);
            this.props.history.push("/dashboard");
          }
        });
    } else {
      event.target.reportValidity();
    }
  };

  render() {
    return (
      <AuthLayout>
        <Content>
          <FlexboxGrid justify="center">
            <FlexboxGrid.Item colspan={12}>
              <Panel className="shadow-box" header={<h3>Sign Up</h3>} bordered>
                <Form fluid id="signInForm" onSubmit={this.handleSubmit}>
                  <FormGroup>
                    <ControlLabel>Name</ControlLabel>
                    <FormControl type="text" name="name" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Email address</ControlLabel>
                    <FormControl type="text" name="email" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Phone number</ControlLabel>
                    <FormControl type="text" name="phoneNumber" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Password</ControlLabel>
                    <FormControl type="text" name="password" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Confirm password</ControlLabel>
                    <FormControl type="text" name="passwordCompare" required />
                  </FormGroup>
                  <div>
                    <FormGroup>
                      <Button type="submit" appearance="primary">
                        Sign un
                      </Button>
                    </FormGroup>
                  </div>
                </Form>
              </Panel>
            </FlexboxGrid.Item>
          </FlexboxGrid>
        </Content>
      </AuthLayout>
    );
  }
}
