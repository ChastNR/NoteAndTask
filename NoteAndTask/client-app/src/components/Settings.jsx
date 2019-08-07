import React from 'react';
import {BoardLayout} from "./layout/BoardLayout";
import {request} from "../libs/api";

export class Settings extends React.Component {
    static displayName = Settings.name;

    constructor(props) {
        super(props);
        this.state = {
            user: null,
            email: null,
            phoneNumber: null
        };
        this.getUser();
    }

    getUser() {
            request('/api/account/getUser')
                .then(data => {
                    this.setState({user: data["name"], email: data["email"], phoneNumber: data["phoneNumber"]})
                })
    }
    
    render() {
        return (
            <BoardLayout>
                <div className="m-3">
                    <dl className="row">
                        <dt className="col-sm-4">
                            Name:
                        </dt>
                        <dd >
                            {this.state.name} 
                        </dd>
                        <dt className="col-sm-4">
                            Email:
                        </dt>
                        <dd >
                           {this.state.email}
                        </dd>
                        <dt className="col-sm-4">
                            Phone number:
                        </dt>
                        <dd>
                            {this.state.phoneNumber}
                        </dd>
                    </dl>
                </div>
            </BoardLayout>
        );
    }
}