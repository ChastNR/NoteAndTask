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
