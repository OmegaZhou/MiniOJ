### User

| 属性     | 类型    |
| -------- | ------- |
| user_id  | int     |
| nickname | varchar |
| password | varchar |

### Problem

| 属性       | 类型   |
| ---------- | ------ |
| problem_id | int    |
| title      | string |
| max_time   | int    |
| max_memory | int    |

### Submission

| 属性          | 类型    |
| ------------- | ------- |
| submission_id | int     |
| user_id       | int     |
| problem_id    | int     |
| status        | int     |
| code          | string  |
| time          | int(ms) |
| memory        | int(Kb) |
| language      | string  |