# Messaging Service

Users should be able to create an account and log in.
Users can message others as long as they know each other's usernames.
Users should be able to access their message history.

## Run Project

Clone project

```bash
  git clone https://github.com/ukutluer/MessagingService.git
```

Go to project folder

```bash
  cd project-folder
```

Run with docker compose

```bash
  docker-compose up -d
```


  
## API Usage


All API requests must have bearerToken for authorization. 
Only User/Register and User/Authenticate are not required for authorization.

Also all API responses returns json format of BaseMessagingServiceResponse. 

#### User Register

```http
  POST /Users/Register
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `UserName` | `string` | **Required**. User name for register. |
| `Password` | `string` | **Required**. User password for register. |

#### Get all users

```http
  GET /Users
```


#### User Authenticate

```http
  POST /Users/Authenticate
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `UserName` | `string` | **Required**. User name for register. |
| `Password` | `string` | **Required**. User password for register. |




#### Send Message

```http
  POST /Message/Send
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `to` | `string` | **Required**. To send message. |
| `content` | `string` | **Required**. To send message text. |


#### Get all messages

```http
  GET /Message/
```


#### Get messages with someone

```http
  POST /Message/{to}
```

| Parametre | Tip     | Açıklama                |
| :-------- | :------- | :------------------------- |
| `to` | `string` | Get message list with username . |




#### Get user login history

```http
  GET /UsersHistory/
```


## Technologies

.Net Core, MongoDB, MongoExpress, SeriLog, Docker


[![GPLv3 License](https://img.shields.io/badge/License-GPL%20v3-yellow.svg)](https://opensource.org/licenses/)