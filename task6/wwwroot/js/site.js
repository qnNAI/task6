let hubConnection;

document.addEventListener('DOMContentLoaded', function () {
    $('#loader').hide();

    $('#f-recipient').autocomplete({
        source: '/user/SearchAutocomplete',
        delay: 500
    });

    hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    setupHubEndpoints();
    hubConnection.start();

    addInfiniteScroll();
    nextPage();
});

function setupHubEndpoints() {
    hubConnection.on("Receive", receiveMsg);

    hubConnection.on("Error", (error) => {
        let msg = document.getElementById('msg-fail');
        msg.innerText = error;
        msg.style.visibility = "visible";
        setTimeout(function() {
            msg.style.visibility = "hidden";
        }, 3000);
    });

    hubConnection.on("Success", () => {
        document.getElementById('send-msg-form').reset();
        let msg = document.getElementById('msg-success');
        msg.style.visibility = "visible";
        setTimeout(function() {
            msg.style.visibility = "hidden";
        }, 3000);
    });
}

function sendMessage() {
    let sub = document.getElementById('Subject');
    let recipient = document.getElementById('f-recipient');
    let content = document.getElementById('Content');

    hubConnection.invoke("Send", {
        "subject": sub.value,
        "content": content.value,
        "recipient": recipient.value
    });
}

function receiveMsg(response, sentTime) {
    createNotification(response);

    createMessageAccordion(response, sentTime);
}


function createMessageAccordion(response, sentTime) {
    let msgAccordion = htmlToElement(`<div class="accordion-item">
        <h2 class="accordion-header">
            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#@message.Id" aria-expanded="false" aria-controls="@message.Id">
                <div class="d-flex flex-column align-items-start">
                    <div class="mb-2">
                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="#305ae6" class="bi bi-envelope me-2" viewBox="0 0 16 16">
                            <path d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V4Zm2-1a1 1 0 0 0-1 1v.217l7 4.2 7-4.2V4a1 1 0 0 0-1-1H2Zm13 2.383-4.708 2.825L15 11.105V5.383Zm-.034 6.876-5.64-3.471L8 9.583l-1.326-.795-5.64 3.47A1 1 0 0 0 2 13h12a1 1 0 0 0 .966-.741ZM1 11.105l4.708-2.897L1 5.383v5.722Z" />
                        </svg>
                        <strong>${response.sender}</strong>, ${sentTime}
                    </div>
                    <span>
                        Subject: <i>${response.subject}</i>
                    </span>
                </div>
             
            </button>
        </h2>
        <div id="@message.Id" class="accordion-collapse collapse" data-bs-parent="#accordionWrapper">
            <div class="accordion-body">
                ${response.content}
            </div>
        </div>
    </div>`);
    let accordionWrapper = document.getElementById('accordionWrapper');
    accordionWrapper.prepend(msgAccordion);
}

function createNotification(response) {
    let toast = htmlToElement(`    <div class="toast" role="alert" aria-live="assertive" aria-atomic="true">
      <div class="toast-header">
        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="blue" class="bi bi-person-fill me-2" viewBox="0 0 16 16">
            <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3Zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z"/>
        </svg>
        <strong class="me-auto">${response.sender}</strong>
        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
      </div>
      <div class="toast-body">
        ${response.subject}
      </div>
    </div>`);


    let toastContainer = document.getElementById('notifs');
    toastContainer.append(toast);
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast);
    toastBootstrap.show();
}

/**
 * @param {String} HTML representing any number of sibling elements
 * @return {NodeList} 
 */
function htmlToElement(html) {
    var template = document.createElement('template');
    html = html.trim();
    template.innerHTML = html;
    return template.content.firstChild;
}

function addInfiniteScroll() {
    let tableWrapper = document.getElementById('infinite');
    tableWrapper.addEventListener("scroll", handleScroll);
}


let scrollTimer;
function handleScroll() {
    clearTimeout(scrollTimer);
    scrollTimer = setTimeout(function () {
        let wrapper = document.getElementById('infinite');
        if (Math.abs(wrapper.scrollHeight - wrapper.clientHeight - wrapper.scrollTop) < 50 && wrapper.scrollTop != 0) {
            nextPage();
        }
    }, 500);
} 

let page = {
    currentPage: 1,
    pageSize: 10
};

function nextPage() {
    loadMessages();
}

function loadMessages() {
    $.ajax({
        beforeSend: () => $('#loader').show(),
        complete: () => $('#loader').hide(),
        url: document.getElementById('infinite').getAttribute('getUrl'),
        type: 'GET',
        cache: false,
        async: true,
        dataType: 'html',
        data: {
            "page": page.currentPage,
            "pageSize": page.pageSize
        }
    }).done(result => {
        page.currentPage++;
        addRows(result);
    });
}

function addRows(result) {
    let body = document.getElementById('accordionWrapper');
    body.innerHTML += result;
}