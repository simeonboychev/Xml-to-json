document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("submitBtn").addEventListener("click", function () {
        var fileInput = document.getElementById("fileInput");
        var newFileNameInput = document.getElementById("newFileNameInput");
        var responseDiv = document.getElementById("response");

        var formData = new FormData();
        formData.append("file", fileInput.files[0]);
        formData.append("fileName", newFileNameInput.value);

        fetch("/api/converter/xml-to-json", {
            method: "POST",
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    return response.text().then(errorMessage => {
                        responseDiv.textContent = errorMessage;
                        console.error("Request failed with error:", errorMessage);
                    });
                }

                return response.json();
            }
            )
            .then(data => {
                if (data) {
                    responseDiv.textContent = `File with name ${data.fileName} was exported successfully.`;
                }
            })
            .catch(error => {
                debugger;
                responseDiv.textContent = "Error: " + error;
            });
    });
});