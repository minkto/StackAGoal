
var goalCardComponent = (function () {

    /**
     * Creates an instance of a Goal Card.
     * @param {number} id
     */
    function GoalCard(id) {
        this.id = id;
        this.goalCardElement = this.getCardElementById(id);
    }

    /**
     * This will get the goal card by it's goal card id attribute.
     * @param {number} id
     */
    GoalCard.prototype.getCardElementById = function (id) {
        var selectedGoalCard = document.querySelector('[data-goal-card-id="' + id + '"]');
        return selectedGoalCard;
    }

    /**
     * Initialise the behaviour of expanding 
     * the Goal Card to show all it's details headers.
     * */
    GoalCard.prototype.initExpandGoalDetails = function () {
        var _self = this;
        var moreGoalDetailsOption = _self.goalCardElement.querySelector('.goal-more-details-btn');
        moreGoalDetailsOption.addEventListener('click', function () {


            var goalsCardDetailsContainer = _self.goalCardElement.querySelector('.goal-card-more-details-container');

            goalsCardDetailsContainer.classList.toggle('show');
            if (goalsCardDetailsContainer.classList.contains('show')) {
                moreGoalDetailsOption.innerHTML = "Less Details <<"
            }
            else {
                moreGoalDetailsOption.innerHTML = "More Details >>"
            }
        }, null)
    }

    /**
     * This will initialise all the expandable header detail
     * events for the selected Goal Card.
     * */
    GoalCard.prototype.expandHeaderDetails = function () {
        //
        var _self = this;
        var expandDetailsButtons = _self.goalCardElement.querySelectorAll('.goal-details-btn');
        for (let i = 0; i < expandDetailsButtons.length; i++) {
            let header = expandDetailsButtons[i].parentElement;
            expandDetailsButtons[i].addEventListener('click', function () {
                header.classList.toggle('hide');
            }, null);
        }
    }

    function initGoalCardCollection()
    {
        let goalCards = document.querySelectorAll('.goal-card');

        for (let i = 0; i < goalCards.length; i++)
        {
            initGoalCard(i);
        }
    }

    /**
     * This will create a set up the event binding
     * for a new Goal Card.
     * @param {number} cardId
     */
    function initGoalCard(cardId) {
        var newGoalCard = new GoalCard(cardId);
        newGoalCard.initExpandGoalDetails();
        newGoalCard.expandHeaderDetails();
    }

    return {

        initComponent: function () {
            initGoalCardCollection();
        }
    }
})();