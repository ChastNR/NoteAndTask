import { action, observable, computed } from "mobx";

import { ITask } from "../interfaces/ITask";
import { IList } from "../interfaces/IList";
import { request } from "../libs/api";

export class ApplicationStore {
    @observable lists: IList[] = [];
    @observable tasks: ITask[] = [];
    @observable tasksWithListId: ITask[] = [];

    @computed
    get tasksState() {
        return this.tasks.filter(task => task.taskListId == undefined);
    }

    @computed
    get taskStateWithListId() {
        return this.tasks.filter(task => task.taskListId != undefined);
    }

    @computed
    get listsState() {
        return this.lists
    }

    @action('loadTasks')
    loadTasks = (id?: number) => {
        if (id) {
            request("/api/task/get?id=" + id).then(data => { this.tasksWithListId = data })
        } else {
            request("/api/task/get").then(data => { this.tasks = data })
        }
    }

    @action('loadLists')
    loadLists = () => {
        request("/api/list/get").then(data => { this.lists = data })
    }
} 