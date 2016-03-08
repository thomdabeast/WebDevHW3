// Need to handle small screens and add 2 yellow squares for good looks
window.onload = createYellows;
window.onresize = createYellows;

var links = [];

function createYellows() {
    var settings = document.getElementById('settings');
	var toDelete = document.getElementById('holder');
	var header = document.getElementById('header');
	var parent = document.getElementById('posts');
	
	if(window.innerWidth <= 810) {
	    
	    if (settings) {
	        links = [];
	        for (var i = 0; i < settings.children.length; ++i)
	        {
	            links.push(settings.children[i].innerHTML);
	        }

			header.removeChild(toDelete);
		
			//Center the heading
			header.children[0].style.float = 'none';
			
			// Create those yellow squares			
			var username = document.createElement('section');
			username.innerHTML = links[0];
			username.className = 'post';
			username.style.background = '#6CF251';
			username.style.textAlign = 'center';
			username.style.fontSize = '26px';
			username.style.color = "#021C40";
			
			if (links.length > 1) {
			    var signing = document.createElement('section');
			    signing.innerHTML = links[1];
			    signing.className = 'post';
			    signing.style.background = '#6CF251';
			    signing.style.textAlign = 'center';
			    signing.style.color = "#021C40";
			}
			if (links.length > 1) {
			    parent.insertBefore(signing, parent.firstChild);
			    parent.insertBefore(username, signing);
			}
			else {
			    parent.insertBefore(username, parent.firstChild);
			}
			
		}
	}
	else {
	    if (!settings) {
			// Float it back left
			header.children[0].style.float = 'left';
			
			var html = '<nav id="holder" style="float: right; margin-right: 12px; "><ul id="settings" style="list-style-type: none">';
		    //Create settings again
			for (var i = 0; i < links.length; ++i)
			{
			    html += '<li class="setting">' + links[i] + '</li>';
			}
	        header.innerHTML += html + '</ul></nav>';
			
	        //Get rid of yellow squares
	        if (links.length > 1) {
	            parent.removeChild(parent.firstChild);
	            parent.removeChild(parent.firstChild);
	        }
	        else parent.removeChild(parent.firstChild);
		}
	}
	
}

function selectedPost(e) {

    var form = document.getElementById('form');
    form.action = "/Posts/See/" + e.id;
    form.submit();
}

function removePost() {
	var e = document.getElementById('currentPost');
	if(e.style.display != "none") {
		document.getElementById('currentPost').removeChild(e.lastChild);
		e.style.display = 'none';
	}
}

function makePost() {
	var parent = document.getElementById('currentPost');
	
	//Create dynamic post
	var post = document.createElement('section');
	post.className = 'currentPost';
	post.style.height = (window.innerHeight - 200) + 'px'; // Compensate for height of browser
	
	var title = document.createElement('input');
	title.type = 'text';
	title.placeholder = 'title';
	title.name = 'title';
	title.style.height = '34px';
	title.style.fontSize = '20px';
	title.style.margin = '30px';
	title.style.padding = '6px';
	
	var body = document.createElement('textarea');
	body.placeholder = 'Enter what you want to brag about (max: 250 words)';
	body.className = 'content';
	body.cols = window.innerWidth / 15; // Rough estimate
	body.rows = 30;
	body.name = 'body';
	body.max = 250;
	body.style.fontSize = '20px';
	body.style.padding = '6px';
	body.style.margin = '30px';
	body.style.marginRight = 0;
	body.style.marginBottom = 0;
	body.style.maxWidth = '92%';
	body.style.maxHeight = '70%';
	
	var button = document.createElement('button');
	button.type = 'submit';
	button.value = 'Create post';
	button.name = 'submit';
	button.style.height = '34px';
	button.style.float = 'right';
	button.className = 'controls';
	
	post.appendChild(title);
	post.appendChild(document.createElement('br'));
	post.appendChild(body);
	post.appendChild(document.createElement('br'));
	post.appendChild(button);
	
	parent.appendChild(post);
	
	parent.style.display = 'block';
}
