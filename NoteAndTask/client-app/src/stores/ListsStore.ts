import { action, observable, computed } from "mobx";

import { IList } from "../interfaces/IList";
import { request } from "../libs/api";

export class ListsStore {
    @observable lists: IList[] = [];

    @computed
    get listsState() {
        return this.lists
    }

    @action('loadLists')
    loadLists = () => {
        request("/api/list/get").then(data => { this.lists = data })
    }
} 