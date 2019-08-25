export const graphQl = async data => {
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
