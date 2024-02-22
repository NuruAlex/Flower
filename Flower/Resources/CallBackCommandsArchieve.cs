using Dialogs;
using Dialogs.AdminDialogs;
using Dialogs.ClientDialogs;
using FileDataBase.Types;
using Messages.Reply.CallBack;
using System;
using System.Collections.Generic;

namespace Flower.Resources;

public static class CallBackCommandsArchieve
{
    private static readonly Dictionary<CallBackCode, Type> _adminDialogs = new()
    {
        { CallBackCode.GameData,               typeof ( PrintGameDataProcess            ) },
        //lists                                 
        { CallBackCode.PromoCodeList,          typeof ( PrintListProcess < PromoCode, string >    ) },
        { CallBackCode.ClientsList,            typeof ( PrintListProcess < Client,long  >    ) },
        { CallBackCode.WishesList,             typeof ( PrintListProcess < Wish   ,int >    ) },
        { CallBackCode.PaymentsList,           typeof ( PrintListProcess < Payment ,int>    ) },
        { CallBackCode.AdminsList,             typeof ( PrintListProcess < Admin   ,long>    ) },
        { CallBackCode.PresentationsList,      typeof ( PrintListProcess < Presentation ,int >    ) }, 
        //items                                 
        { CallBackCode.Promocode,              typeof ( PrintItemProcess < PromoCode    , string >    ) },
        { CallBackCode.Client,                 typeof ( PrintItemProcess < Client       , long >    ) },
        { CallBackCode.Wish,                   typeof ( PrintItemProcess < Wish         , int  >    ) },
        { CallBackCode.Payment,                typeof ( PrintItemProcess < Payment      , int >    ) },
        { CallBackCode.Admin,                  typeof ( PrintItemProcess < Admin        , long  >    ) },
        { CallBackCode.Presentation,           typeof ( PrintItemProcess < Presentation , int  >    ) },
        //delete
        { CallBackCode.DeleteClient,           typeof ( DeleteItemProcess < Client  , long > ) },
        { CallBackCode.DeleteAdmin,            typeof ( DeleteItemProcess < Admin   , long>  ) },
        { CallBackCode.DeletePromoCode,        typeof ( DeleteItemProcess<PromoCode , string>     ) },
        //other
        { CallBackCode.MoreDetailes,           typeof ( MoreDetailsDialog         ) },
        { CallBackCode.WriteToClient,          typeof ( WriteToClientDialog       ) },
        { CallBackCode.ClientStatistics,       typeof ( StaticsticsDialog         ) },
        { CallBackCode.ClientSummary,          typeof ( SummaryDialog             ) },
        { CallBackCode.PromoCodeInfo,          typeof ( PromoCodeInfoDialog       ) },
        { CallBackCode.AddPromoCode,           typeof ( NewPromoCodeDialog        ) },
        { CallBackCode.AddDiscontPromoCode,    typeof ( NewDiscontPromoCodeDialog ) },
        { CallBackCode.AddSpecialPromoCode,    typeof ( NewSpecialPromocodeDialog ) },
        { CallBackCode.MakeNewAdmin,           typeof ( NewAdminDialog            ) },
        { CallBackCode.GlobalStatistics,       typeof ( PrintGlobalStatisticsProcess    ) },
        { CallBackCode.AnswerPresentation,     typeof ( AnswerPresetationDialog   ) },
        { CallBackCode.AnswerWish,             typeof ( AnswerWishDialog          ) },
        { CallBackCode.DenyPaymentRequest,     typeof ( DenyPaymentRequestDialog  ) },
        { CallBackCode.AprovePaymentRequest,   typeof ( AprovePaymentDialog       ) },
        //client
        
        { CallBackCode.CreateWish,           typeof( WishProcess)},
        { CallBackCode.CreatePresentation,   typeof( PresentationProcess)},
        { CallBackCode.CreateChoice,         typeof( SelectCardProcess)},
        { CallBackCode.BotPromocode,         typeof( PromocodeDialog) },
    };


    public static IStartProcess GetProcess(CallBackCode callBack)
    {
        if (_adminDialogs.TryGetValue(callBack, out var dialogType))
            return (IStartProcess)Activator.CreateInstance(dialogType);

        return new UnknownProcess();
    }


}
