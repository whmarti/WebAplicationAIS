/****************************************************************
 * Developer: William Martin
 * Last modification: 03/19/2021
 * Class function: Class used to manage auxiliary data structures, 
 * such as enumerations, dictionaries, etc.
 * Company: AIS - NZ
 * */
using System;



/// <summary>
/// Enumeration used to manage the degree of criticality of a message.
/// </summary>  
public enum WarningType
{
    Success,
    Info,
    Warning,
    Danger
}