**[Virtual Auction House - Unity VR in multiplayer world]{.underline}**

To play the game just download the apk file provided in the folder. And
use Google Cardboard along with the Bluetooth controller provided by the
professor. The target device is an Android phone. Android versions of
the mobile phone should be greater than or equal to Android KitKat.

We have 3 scenes in our auction house VR project.

1.  **Lobby scene:** This is the starting scene of our project where the
    user enters their information and enters the auction house. We have
    created a registration canvas with 3 input fields and ask the
    user\'s Name, phone number and email id to let the user join in the
    auction house. Users also have the option to select from 2 avatars
    on the login page. When the user gazes on any text field and selects
    it a canvas keyboard opens. Users can gaze on the keys and use the
    "B" button on the controller to type. Users can join the auction
    house clicking the join button and selecting the desired avatar.

2.  **Game Scene:** This is our main scene, it contains our auction
    house and auction items and all the interaction and bidding takes
    place in this scene. There are 6 auction items placed for Auction:
    Car, Spartan, Robot, Drone, Boat, Vanity desk. Each auction item
    except Car has some interaction available for the user (for now).
    There is also an Auction board and watchlist for each user. Next to
    each item we have an auction info panel which shows the item's
    current bid and current bidder, a Bid Button and Add To Watchlist
    button. The user can click on the bid button to open the bidding
    window. In the bidding window they increase the bid of the item by
    50, 100, 500 or 1000 and the info panel updates to show the most
    recent bid. The user has the option to add or remove any item in his
    watchlist. If the price of any item that is present in the watchlist
    changes the user will get a notification. Also the auction house is
    multiplayer and the bidding system is getting updated in real time.
    A maximum of 4 people can join the auction house, above that there
    is a cost associated to incorporate more users. This auction is
    timed and all the users will exit the auction house when the set
    time is elapsed.

> **Boat Ride:** When a user clicks (button 'B') on the boat, the user
> can test drive the boat in the water. The user can return back to the
> auction house by clicking the 'B' button again.

3.  **Checkout Scene:** The auction house is open only for a fixed
    period, after that each user will be checked out to their shopping
    cart and can pay for the items where they are the highest bidder.
    The checkout scene has two buttons to pay and get back to the lobby.
    The user can also willingly exit the action house and go to the
    checkout scene by pressing the menu button.

**[Interaction Techniques]{.underline} : (**Gaze based and controller
for selection)

The user wears a google cardboard and handheld controller (provided by
the professor) to perform action in the auction house app. To move
around - the joystick controller is used. Users can turn in any
direction using gaze based movement. And to interact with any item, the
user has to gaze at the item and click the controller\'s B button to
play animation for the item. Eg: For a robot when the user gazes over it
and clicks B robot opens up and spins around. And for the boat when the
user gazes and clicks B the user can do the test drive by using the
joystick and can drive the boat in any direction over water.

Auction board/ watchlist is opened when the user presses the Y Button on
the controller. When the auction board is open the movement of the
player is restricted. Also auction board and watchlist is not visible to
other players in the game.

**[Multi-player]{.underline}**

We are using a "photon unity networking 2 (PUN2)" framework for
multiplayer interaction and data synchronization. In our auction house
for now upto 4 people can join for free. A user just has to enter their
information in the lobby, choose from the available avatars and click
join. Once inside users will be able to see each other and when a user
increases bid, other users will be able to see that in real time (with
minimum lag) with the name of the current bidder. Thus, the bidding
system is working in real-time across multi-user.

**[Multiplayer Voice Interaction]{.underline}**

With the help of the "photon unity networking 2 voice (PUN2 voice)"
framework, a user can interact with all the other users of the auction
by holding down the "x" button. As long as the "x" button is pressed,
whatever a user says, his/her voice is streamlined to all the other
users in the auction house and everyone in the house can listen to that
voice.

**NOTE:** To enable voice interaction, the user first has to give the
microphone access permission to the installed application before
launching the app in the phone.

**[Our 3 preliminary deliverables were:]{.underline}**

1.  Build auction space and auction items.

-   One Auction Space

-   6 Auction items in the house

2.  Let the users interact with the auction item

-   A panel next to each item which has information regarding the item,
    current bid, current bidder etc.

3.  Bidding on the item.

-   The user can use the bid option to bid on the item using the
    Interactive Bidding System.

-   The interactive bidding system will have some bids on which the user
    can click using the controller and increase the bid.

-   The increased bid and the current bidder is updated locally.

**[Our three final deliverables were:]{.underline}**

-   The backend should be complete.

    -   The user data gets stored in the backend and gets displayed
        using the UI elements.

-   Multiplayer Platform

    -   Support connection between multiple Google Cardboard users via
        Unity.

    -   Any change that a user makes in the bid will be reflected in
        Real Time to the other users.

    -   Voice Interaction between users.

        -   Players can interact using voice chats in the Auction house.

-   Users watchlist and shopping cart

    -   Users can add the items in the watchlist and get notifications
        when the price increases.

    -   The items bought by the user will be shown in individual
        shopping cart.

    -   Real Time Updates

        -   The users will be able to see the current bidding price on
            each item.

**[Extra deliverables:]{.underline}**

1.  Multiplayer sync: Upto 4 players can join the game and interact and
    bid on the item at the same time and their movement is synced.

2.  Player Animation: We can see the animation (movement) of the other
    player when he/she is moving in the game.

3.  Visual Cues for intractable Auction items.

4.  Animation for various auction items.

5.  We can do a test drive for one of the auction items (Boat) in the
    auction house.
