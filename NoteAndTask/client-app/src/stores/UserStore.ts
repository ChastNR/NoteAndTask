import { action, observable, computed } from "mobx";

import { request } from "../libs/api";

export class UserStore {
    @observable name: string = "";
    @observable email: string = "";
    @observable phoneNumber: string = "";


    @computed
    get userState() {
        return this.name + this.email + this.phoneNumber
    }

    @action('getUser')
    getUser = () => {
        request("/api/account/getuser").then(data => { this.name = data["name"]; this.email = data["email"]; this.phoneNumber = data["phoneNumber"] })
    }
}