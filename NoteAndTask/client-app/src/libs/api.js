//Default fetch
export const request = async (url, method = "get", data) => {
  const response = await fetch(url, {
    method,
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token")
    },
    body: data
  });
  if (response.status === 200) {
    return response.json();
  }
  if (response.status === 401) {
    localStorage.removeItem("token");
    window.location.href = "/signin";
  }
};
//Default fetch for GraphQL
export const graphQl = async (data) => {
  const response = await fetch("/api/data", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + localStorage.getItem("token")
    },
    body: JSON.stringify(data)
  });
  if (response.status === 200) {
    return response.json();
  }
  if (response.status === 401) {
    localStorage.removeItem("token");
    window.location.href = "/signin";
  }
};

//Tasks api
/*
export const loadTasks = id => {
  if (id) {
    return request("/api/task/get?id=" + id);
  } else {
    return request("/api/task/get");
  }
};
*/
