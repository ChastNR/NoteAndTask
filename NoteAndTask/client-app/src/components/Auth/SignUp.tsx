import * as React from "react";
import { AuthLayout } from "../layout/AuthLayout";
import { Button, Content, ControlLabel, FlexboxGrid, Form, FormGroup, Panel } from "rsuite";

interface IRegisterModel {
  name: string;
  email: string;
  phoneNumber: string;
  password: string;
  passwordCompare: string;
}

export class SignUp extends React.Component<any> {
  handleSubmit = async (event: any) => {
    event.preventDefault();

    if (event.target.checkValidity()) {
      let formData: IRegisterModel = {
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
                    <input className="rs-input" type="text" name="name" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Email address</ControlLabel>
                    <input className="rs-input" type="text" name="email" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Phone number</ControlLabel>
                    <input className="rs-input" type="text" name="phoneNumber" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Password</ControlLabel>
                    <input className="rs-input" type="text" name="password" required />
                  </FormGroup>
                  <FormGroup>
                    <ControlLabel>Confirm password</ControlLabel>
                    <input className="rs-input" type="text" name="passwordCompare" required />
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
