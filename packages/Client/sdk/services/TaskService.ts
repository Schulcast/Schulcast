import { Api, Task } from 'sdk'

export class TaskService {
	static get(id: number) {
		return Api.get<Task>(`/task/${id}`)
	}

	static getAll() {
		return Api.get<Array<Task>>(`/task`)
	}

	static save(task: Task) {
		return !task.id
			? Api.post<Task>('/task', task)
			: Api.put<Task>(`/task`, task)
	}

	static delete(task: Task) {
		return Api.delete(`/task/${task.id}`)
	}
}