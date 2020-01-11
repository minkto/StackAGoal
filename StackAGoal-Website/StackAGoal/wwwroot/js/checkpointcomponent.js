var checkpointModule = (function () {


    /** This will hold all the loaded model checkpoints. */
    let CheckpointHashTable = {};

    /**
     * This represents a Cehckpoint Model Object.
     * @param {any} id - Id of the checkpoint
     * @param {any} description - description of the checkpoint
     * @param {any} isComplete - Is the checkpoint complete or not.
     * @param {any} goalId - The goal for whoch this checkpoint belongs to.
     */
    function CheckpointItem(id,description,isComplete,goalId)
    {
        this.id = id;
        this.description = description;
        this.isComplete = isComplete;
        this.goalId = goalId;
    }

    function onDescriptionEditModeFocusOut(descriptionBox) {
        descriptionBox.stopPropagation();
        var editSaveBtn = descriptionBox.target.nextElementSibling.nextElementSibling;


        // Only allow the focus out to fire once. If not edit mode is useless.
        descriptionBox.target.removeEventListener('focusout', onDescriptionEditModeFocusOut);
        descriptionBox.target.setAttribute('readonly', 'true');
        descriptionBox.target.classList.toggle('readonly');  

    }

    function showMenu(sourceElement,id) {

        //removeExistingMenu();
        let checkpoint = CheckpointHashTable["checkpoint-" + id];
        var popupHtml = `<div class="checkpoint-edit-menu">
                  <div class="checkpoint-header">
                    <p>Manage Checkpoint</p>
                  </div>
                <ul>
                  <li><a href="#" class="js-edit-checkpoint">Edit</a> </li>
                  <li><a href="#" class="js-delete-checkpoint">Delete</a></li>
                </ul>
                </div>`
        sourceElement.insertAdjacentHTML('beforeend', popupHtml);
        var insertedEl = sourceElement.lastChild;
        var editMenu = insertedEl.querySelector('.js-edit-checkpoint');
        var deleteMenu = insertedEl.querySelector('.js-delete-checkpoint');
        var container = editMenu.parentNode.parentNode.parentNode.parentNode.parentNode;


        // Initialise menu events
        editMenu.addEventListener('click', function (e) {
            e.stopPropagation();

            var descriptionBox, btnSaveEdit;

            // Start edit.
            descriptionBox = container.querySelector('.checkpoint-description');
            descriptionBox.removeAttribute('readonly');
            descriptionBox.classList.toggle('readonly');

            btnSaveEdit = container.querySelector('.js-checkpoint-edit-save');
            btnSaveEdit.classList.toggle('editmode');

            descriptionBox.focus();

            // when leaving, disable again.
            descriptionBox.addEventListener('focusout', onDescriptionEditModeFocusOut);

        });

        deleteMenu.addEventListener('click', function (e) {

            e.stopPropagation();

            deleteCheckpointAJAX(checkpoint);
            container.classList.toggle('deleted');
            setTimeout(function () {
                container.remove();
            }, 200);
        });

    }

    function removeExistingMenu() {
        var pop = document.querySelector('.checkpoint-edit-menu');
        if (pop !== null) {
            pop.style.display = 'none';
            pop.remove();
        }
    }

    /**
     * Initialise the menu used to manage the checkpoint.
     * */
    function initClickOutsideMenu() {
        window.addEventListener('mouseup', function (event) {

            var checkpointMenu = document.querySelector('.checkpoint-edit-menu');
            if (event.target != checkpointMenu && event.target.parentNode != checkpointMenu) {
                if (checkpointMenu !== null) {
                    checkpointMenu.style.display = 'none';
                }
            }
        });
    }

    /**
     * Sets up the menu that will show when the manage icon
     * is clicked upon.
     * @param {any} editBoxNode - The Icon for managing a checkpoint.
     * @param {any} id - Id of the checkpoint
     */
    function setupCheckpointMenu(editBoxNode,checkpointId) {
        editBoxNode.addEventListener('click', function (e) {
            removeExistingMenu();
            showMenu(editBoxNode, checkpointId);
        });
    }

    /**
     * Creates the UI for the Checkpoint List Component
     * @param {any} checkpoint The checkpoint to create a component.
     */
    function createCheckpointUIComponent(checkpoint)
    {
        var checkPointList = document.querySelector('.checkpoint-list');
        var checkPointContainerElement = document.createElement('div');
        var checkPointEditBox = document.createElement('div');
        var checkPointDescription = document.createElement('textarea');
        var checkPointDoneBox = document.createElement('div');
        var checkpointEditSaveBtn = document.createElement('input');

        checkPointContainerElement.className = 'checkpoint-item-container';

        // Set ID
        checkPointContainerElement.id = 'checkpoint-' + checkpoint.id;

        checkPointEditBox.className = 'edit-box';
        checkPointEditBox.insertAdjacentHTML('beforeend', '<span class="icon icon-pencil-square-o"></span>');

        setupCheckpointMenu(checkPointEditBox, checkpoint.id);

        checkPointContainerElement.appendChild(checkPointEditBox);
        checkPointDescription.className = 'checkpoint-description readonly';

        // Set Description
        checkPointDescription.innerHTML = checkpoint.description;
        checkPointDescription.setAttribute('readonly', 'true');
        checkPointContainerElement.appendChild(checkPointDescription);

        checkPointDoneBox.className = 'checkpoint-done';

        // Set checkpoint is complete.
        checkPointDoneBox.insertAdjacentHTML('beforeend', setIsCompleteIcon(checkpoint.isComplete));
        checkPointDoneBox.addEventListener('click', function ()
        {
            // update model
            checkpoint.isComplete = !checkpoint.isComplete;
            var icon = checkPointDoneBox.firstChild;

            // call ajax
            updateCheckpointAJAX(checkpoint);


            // update flag icon
            if (!icon.classList.contains('done')) {
                icon.className = 'icon-check-square-o done';
                console.log('toggle done')
            } else {
                icon.className = 'icon-square-o';
            }   

        });

        //Add the save button.
        checkpointEditSaveBtn.setAttribute('type', 'button');
        checkpointEditSaveBtn.setAttribute('value', 'Save');
        checkpointEditSaveBtn.className = 'btn btn-primary checkpoint-edit-save-btn js-checkpoint-edit-save';
        checkpointEditSaveBtn.addEventListener('click', function () {

            checkpoint.description = checkPointDescription.value;
            updateCheckpointAJAX(checkpoint);
            checkpointEditSaveBtn.classList.toggle('editmode');
        });

        // Build the main component.
        checkPointContainerElement.appendChild(checkPointDescription);
        checkPointContainerElement.appendChild(checkPointDoneBox);
        checkPointContainerElement.appendChild(checkpointEditSaveBtn);

        // Add the latest checkpoint to the list
        checkPointList.insertBefore(checkPointContainerElement, checkPointList.firstChild);

    }

    /**
     * Returns a DOM string representing a complete checkpoint.
     * @param {any} isComplete
     */
    function setIsCompleteIcon(isComplete) {
        if (isComplete) {
            return '<div class="icon-check-square-o done"></div>'
        }
        else {
            return '<div class="icon-square-o"></div>'
        }
    }

    function init()
    {
        initClickOutsideMenu();
    }

    function reset() {
        var checkpointList = document.querySelector('.checkpoint-list');
        while (checkpointList.firstChild) {
            checkpointList.removeChild(checkpointList.firstChild);
        }

        CheckpointHashTable = {};
    }

    function updateCheckpointAJAX(checkpoint)
    {
        console.log('Updating checkpoint....');
        $.ajax({
            url: "/Checkpoints/UpdateCheckpointsByGoal",
            method: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(checkpoint),
            success: function (data) {
                if (data > 0) {
                    console.log('checkpoint has been updated.');
                }
            }
        });
    }

    function deleteCheckpointAJAX(checkpoint) {
        var id = checkpoint.id;

        console.log('Deleting Checkpoint.');
        console.log(checkpoint);
        console.log(checkpoint.id);
        $.ajax({
            url: "/Checkpoints/DeleteCheckpoint?id=" + id,
            method: 'DELETE',
            contentType: 'application/json; charset=utf-8',
            data: id,
            success: function (data) {
                if (data > 0) {
                    console.log('checkpoint has been successfully deleted.');
                }
            }
        });
    }

    return {
        initComponent: function () {
            init();
        },
        resetComponent: function ()
        {
            reset();
        },
        createItem: function (id, description, isComplete) {
            CheckpointHashTable["checkpoint-" + id] = new CheckpointItem(id, description, isComplete);
            createCheckpointUIComponent(CheckpointHashTable["checkpoint-" + id]);
        }
    }

});