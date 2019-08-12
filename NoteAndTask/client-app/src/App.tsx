import React from "react";
import {Route, Switch} from "react-router";
import {Tasks} from "./components/Tasks";
import {Archive} from "./components/Archive";
import {AuthLayout} from "./components/layout/AuthLayout";
import {SignIn} from "./components/SignIn";
import {SignUp} from "./components/SignUp";
import {Settings} from "./components/Settings";
import {Notes} from "./components/Notes";
import {DashBoard} from "./components/layout/DashBoard";
import {Lists} from "./components/Lists";

const App: React.FC = () => {
    return (
        <Switch>
            <Route exact path="/" component={AuthLayout}/>
            <Route path="/tasks/:id" component={Tasks}/>
            <Route path="/tasks" component={Tasks}/>
            <Route path="/signin" component={SignIn}/>
            <Route path="/signup" component={SignUp}/>
            <Route path="/archive" component={Archive}/>
            <Route path="/notes" component={Notes}/>
            <Route path="/dashboard" component={DashBoard}/>
            <Route path="/lists" component={Lists}/>
            <Route path="/settings" component={Settings}/>
        </Switch>
    );
};

export default App;