import { action, observable, computed } from "mobx";

import { ITask } from "../interfaces/ITask";
import { request } from "../libs/api";

export class TasksStore {
    @observable tasks: ITask[] = [];

    @computed
    get tasksState() {
        return this.tasks;
    }

    @computed
    get archiveState() {
        return this.tasks.filter(task => task.isDone);
    }

    @action('loadTasks')
    loadTasks = () => {
        request("/api/task/getall").then(data => { this.tasks = data })
    };

    @action('loadArchivedTasks')
    loadArchivedTasks() {
        request("/api/task/get?archived=true").then(data => { this.tasks = data })
    }
} 