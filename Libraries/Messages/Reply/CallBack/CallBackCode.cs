namespace Messages.Reply.CallBack;

public enum CallBackCode
{
    Ingnore,
    //game data
    GameData,//readonly

    GlobalStatistics,  //^ to game data


    WishesList,        //^ to game data
    Wish,                        //^ to list
    AnswerWish,                  //^ to list
    DenyWish,                    //^ to list

    PresentationsList, //^ to game data
    Presentation,                //^ to list
    AnswerPresentation,          //^ to list


    ClientsList,       //^ to game data
    Client,                      //^ to list
    ClientStatistics,               //^ to client
    ClientSummary,                  //^ to client
    MoreDetailes,                   //^ to client
    DeleteClient,                //^ to list
    WriteToClient,                  //^ to client


    PaymentsList,      //^ to game data
    Payment,                     //^ to list
    DenyPaymentRequest,          //^ to list
    AprovePaymentRequest,        //^ to list


    PromoCodeList,     //^ to game data
    Promocode,                   //^ to list
    AddPromoCode,                //^ to list
    AddSpecialPromoCode,         //^ to list
    AddDiscontPromoCode,         //^ to list
    DeletePromoCode,             //^ to list
    PromoCodeInfo,               //^ to list

    AdminsList,        //^ to game data
    Admin,                    //^ to list
    DeleteAdmin,
    MakeNewAdmin,




    Continue,

    BotPromocode,
    ChooseBotGame,
    ChooseIndividualGame,
    ChooseGroupGame,

    PayBot,
    PayGroup,
    PayIndividual,

    CreateWish,
    CreatePresentation,
    CreateChoice
}
