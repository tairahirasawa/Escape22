using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EventAction))]
public class EventActionDrawer : PropertyDrawer
{
    string eventActionType = "eventActionType";
    string fadeIn = "fadeIn";
    string fadeOut = "fadeOut";
    string waitTime = "waitTime";
    string debugEventAction = "debugEventAction";
    string messageEventAction = "messageEventAction";
    string zoomEventAction = "zoomEventAction";
    string setActiveEventAction = "setActiveEventAction";
    string changeMapEventAction = "changeMapEventAction";
    string itemGetEventAction = "itemGetEventAction";
    string itemRemoveEventAction = "itemRemoveEventAction";
    string colorChangeAction = "colorChangeAction";
    string colorTwinkleAction = "colorTwinkleAction";
    string playSeEventAction = "playSeEventAction";
    string showAdEventAction = "showAdEventAction";
    string mapCustomEventAction = "mapCustomEventAction";
    string autoZoomBackEventAction = "autoZoomBackEventAction";
    string objectChangeAnimationEventAction = "objectChangeAnimationEventAction";
    string cameraMoveEventAction = "cameraMoveEventAction";
    string unLockOpenCloseObjectEventAction = "unLockOpenCloseObjectEventAction";
    string changeCameraCurrentIndexEventAction = "changeCameraCurrentIndexEventAction";
    string endingSuccessEventAction = "endingSuccessEventAction";
    string playBGMEventAction = "playChangeBGMEventAction";
    string stopBgmEventAction = "stopBgmEventAction";
    string storeReviewEventAction = "storeReviewEventAction";
    string shakeCameraEventAction = "shakeCameraEventAction";
    string objectMoveEventAction = "objectMoveEventAction";
    string playTimelineEventAction = "playTimelineEventAction";
    string endingFailEventAction = "endingFailEventAction";
    string counterPlusEventAction = "counterPlusEventAction";
    string dismissItemWindowEventAction = "dismissItemWindowEventAction";
    string collectionItemGetEvent = "collectionItemGetEvent";
    string eventActionEvent = "eventActionEvent";
    string objectRotationEventAction = "objectRotationEventAction";
    string presentCloseUpPanel = "presentCloseUpPanel";
    string objectScaleEventAction = "objectScaleEventAction";
    string OnClickBackButtonEventAction = "OnClickBackButtonEventAction";
    string stopSeEventAction = "stopSeEventAction";
    string progressFalse = "progressFalse";
    string presentGimmickEventAction = "presentGimmickEventAction";
    string changeWhetherAndTimeEventAction = "changeWhetherAndTimeEventAction";
    string changeEpisodeEventAction = "changeEpisodeEventAction";
    string nonSelectItemEventAction = "nonSelectItemEventAction";
    string endStageEventAction = "endStageEventAction";
    string presentGimmickByName = "presentGimmickByName";
    string changeItemSpriteEventAction = "changeItemSpriteEventAction";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // eventTypeプロパティを取得し、常に表示
        SerializedProperty eventType = property.FindPropertyRelative(eventActionType);
        EditorGUI.PropertyField(position, eventType, new GUIContent("Event Type"), true);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;


        // eventTypeがfadeの場合のみ、testとtest2プロパティを含むその全子プロパティを表示
        if (eventType.enumValueIndex == (int)EventActionType.fadeIn) PresentProperty(fadeIn, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.fadeout) PresentProperty(fadeOut, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.waitTime) PresentProperty(waitTime, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.debug) PresentProperty(debugEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.message) PresentProperty(messageEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.zoom) PresentProperty(zoomEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.setActive) PresentProperty(setActiveEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.changeMap) PresentProperty(changeMapEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.itemGet) PresentProperty(itemGetEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.itemRemove) PresentProperty(itemRemoveEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ColorChange) PresentProperty(colorChangeAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ColorTwinkle) PresentProperty(colorTwinkleAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PlaySE) PresentProperty(playSeEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ShowAd) PresentProperty(showAdEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.MapCustom) PresentProperty(mapCustomEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.AutoZoomBack) PresentProperty(autoZoomBackEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ObjectChangeAnimation) PresentProperty(objectChangeAnimationEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.CameraMove) PresentProperty(cameraMoveEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.UnLockOpenCloseObject) PresentProperty(unLockOpenCloseObjectEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeCameraCurrentIndex) PresentProperty(changeCameraCurrentIndexEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.EndingSuccess) PresentProperty(endingSuccessEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PlayChangeBGM) PresentProperty(playBGMEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.StopBGM) PresentProperty(stopBgmEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.StoreReview) PresentProperty(storeReviewEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ShakeCamera) PresentProperty(shakeCameraEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.objectMove) PresentProperty(objectMoveEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PlayTimeline) PresentProperty(playTimelineEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.EndingFail) PresentProperty(endingFailEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.CounterPlus) PresentProperty(counterPlusEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.DismissItemWindow) PresentProperty(dismissItemWindowEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.CollectionItemGet) PresentProperty(collectionItemGetEvent, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.EventActionEvent) PresentProperty(eventActionEvent, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ObjectRotation) PresentProperty(objectRotationEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PresentCloseUpPanel) PresentProperty(presentCloseUpPanel, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.objectScale) PresentProperty(objectScaleEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.OnClickBackButtonAction) PresentProperty(OnClickBackButtonEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.StopSE) PresentProperty(stopSeEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ProgressFalse) PresentProperty(progressFalse, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PresentGimmick) PresentProperty(presentGimmickEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeWhetherAndTime) PresentProperty(changeWhetherAndTimeEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeEpisode) PresentProperty(changeEpisodeEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.NonSelectItem) PresentProperty(nonSelectItemEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.endStage) PresentProperty(endStageEventAction, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.PresentGimmickByName) PresentProperty(presentGimmickByName, position, property);
        if (eventType.enumValueIndex == (int)EventActionType.changeItemSprite) PresentProperty(changeItemSpriteEventAction, position, property);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float propertyHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        SerializedProperty eventType = property.FindPropertyRelative(eventActionType);

        if (eventType.enumValueIndex == (int)EventActionType.fadeIn) propertyHeight += Hogeee(property,fadeIn);
        if (eventType.enumValueIndex == (int)EventActionType.fadeout) propertyHeight += Hogeee(property, fadeOut);
        if (eventType.enumValueIndex == (int)EventActionType.waitTime) propertyHeight += Hogeee(property, waitTime);
        if (eventType.enumValueIndex == (int)EventActionType.debug) propertyHeight += Hogeee(property, debugEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.message) propertyHeight += Hogeee(property, messageEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.zoom) propertyHeight += Hogeee(property, zoomEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.setActive) propertyHeight += Hogeee(property, setActiveEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.changeMap) propertyHeight += Hogeee(property, changeMapEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.itemGet) propertyHeight += Hogeee(property, itemGetEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.itemRemove) propertyHeight += Hogeee(property, itemRemoveEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ColorChange) propertyHeight += Hogeee(property, colorChangeAction);
        if (eventType.enumValueIndex == (int)EventActionType.ColorTwinkle) propertyHeight += Hogeee(property, colorTwinkleAction);
        if (eventType.enumValueIndex == (int)EventActionType.PlaySE) propertyHeight += Hogeee(property, playSeEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ShowAd) propertyHeight += Hogeee(property, showAdEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.MapCustom) propertyHeight += Hogeee(property, mapCustomEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.AutoZoomBack) propertyHeight += Hogeee(property, autoZoomBackEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ObjectChangeAnimation) propertyHeight += Hogeee(property,objectChangeAnimationEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.CameraMove) propertyHeight += Hogeee(property, cameraMoveEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.UnLockOpenCloseObject) propertyHeight += Hogeee(property, unLockOpenCloseObjectEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeCameraCurrentIndex) propertyHeight += Hogeee(property, changeCameraCurrentIndexEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.EndingSuccess) propertyHeight += Hogeee(property, endingSuccessEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.PlayChangeBGM) propertyHeight += Hogeee(property, playBGMEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.StopBGM) propertyHeight += Hogeee(property, stopBgmEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.StoreReview) propertyHeight += Hogeee(property,storeReviewEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ShakeCamera) propertyHeight += Hogeee(property, shakeCameraEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.objectMove) propertyHeight += Hogeee(property, objectMoveEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.PlayTimeline) propertyHeight += Hogeee(property, playTimelineEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.EndingFail) propertyHeight += Hogeee(property, endingFailEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.CounterPlus) propertyHeight += Hogeee(property, counterPlusEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.DismissItemWindow) propertyHeight += Hogeee(property, dismissItemWindowEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.CollectionItemGet) propertyHeight += Hogeee(property, collectionItemGetEvent);
        if (eventType.enumValueIndex == (int)EventActionType.EventActionEvent) propertyHeight += Hogeee(property, eventActionEvent);
        if (eventType.enumValueIndex == (int)EventActionType.ObjectRotation) propertyHeight += Hogeee(property, objectRotationEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.PresentCloseUpPanel) propertyHeight += Hogeee(property, presentCloseUpPanel);
        if (eventType.enumValueIndex == (int)EventActionType.objectScale) propertyHeight += Hogeee(property, objectScaleEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.OnClickBackButtonAction) propertyHeight += Hogeee(property, OnClickBackButtonEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.StopSE) propertyHeight += Hogeee(property, stopSeEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ProgressFalse) propertyHeight += Hogeee(property, progressFalse);
        if (eventType.enumValueIndex == (int)EventActionType.PresentGimmick) propertyHeight += Hogeee(property, presentGimmickEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeWhetherAndTime) propertyHeight += Hogeee(property, changeWhetherAndTimeEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.ChangeEpisode) propertyHeight += Hogeee(property, changeEpisodeEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.NonSelectItem) propertyHeight += Hogeee(property, nonSelectItemEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.endStage) propertyHeight += Hogeee(property, endStageEventAction);
        if (eventType.enumValueIndex == (int)EventActionType.PresentGimmickByName) propertyHeight += Hogeee(property, presentGimmickByName);
        if (eventType.enumValueIndex == (int)EventActionType.changeItemSprite) propertyHeight += Hogeee(property, changeItemSpriteEventAction);

        return propertyHeight;
    }

    public void PresentProperty(string name , Rect position, SerializedProperty property)
    {
        SerializedProperty test = property.FindPropertyRelative(name);
        EditorGUI.PropertyField(new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(test, true)), test, new GUIContent(name), true);
    }
    public float Hogeee(SerializedProperty property, string name)
    {
        SerializedProperty test = property.FindPropertyRelative(name);
        return EditorGUI.GetPropertyHeight(test, true) + EditorGUIUtility.standardVerticalSpacing;
    }

}