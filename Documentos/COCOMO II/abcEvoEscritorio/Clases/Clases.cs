using ABCEvoEscritorio.Clases;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCEvoEscritorio.Clases
{
    public class Usuario
    {
        public Int32 idEmployee { get; set; }
        public string name { get; set; }

        public string email { get; set; }

        public string telephone { get; set; }

        public string jobPosition { get; set; }

        public bool status { get; set; }

        public string photoUrl { get; set; }

    }

    public class Sale
    {
        public Int32? idSale { get; set; }

        public Int32? idMember { get; set; }

        public Int32? idEmployee { get; set; }

        public Int32? idProspect { get; set; }

        public Int32? idEmployeeSale { get; set; }

        public DateTime? saleDate { get; set; }

        public DateTime? saleDateServer { get; set; }

        public Int32? idPersonal { get; set; }

        public string? corporatePartnershipName { get; set; }

        public Int32? coporatePartnershipId { get; set; }

        public bool? removed { get; set; }

        public Int32? idEmployeeRemoval { get; set; }

        public DateTime? removalDate { get; set; }

        public Int32? idBranch { get; set; }

        public string? observations { get; set; }

        public Int32? idSaleRecurrency { get; set; }

        public Int32? saleSource { get; set; }

        public string? idSaleMigration { get; set; }

        public List<saleItem>? saleItens { get; set; }

        public List<receivable>? receivables { get; set; }

    }


    public class filaItem
    {



        public string? description { get; set; }

        public string? item { get; set; }
        public decimal? itemValue { get; set; }
        public decimal? tax { get; set; }

        public decimal? saleValue { get; set; }

        public Int32? quantity { get; set; }

        public string? idQbItem { get; set; }

        public decimal? discount { get; set; }


    }

    public class filaPago
    {
 

        public decimal? ammount { get; set; }

        public decimal? ammountPaid { get; set; }

        public string? cardFlag { get; set; }
        public string? QbMethod { get; set; }
        public string? authorization { get; set; }
        public Boolean procesado { get; set; }
        public DateTime? receivingDate { get; set; }

    }

    public class saleItem
    {

        public Int32? idSaleItem { get; set; }

        public string? description { get; set; }

        public string? item { get; set; }
        public decimal? itemValue { get; set; }

        public decimal? saleValue { get; set; }

        public decimal? saleValueWithoutCreditValue { get; set; }

        public Int32? quantity { get; set; }

        public Int32? idMembership { get; set; }

        public Int32? idMembershipRenewed { get; set; }

        public Int32? numMembers { get; set; }

        public Int32? idProduct { get; set; }

        public Int32? idService { get; set; }

        public string? corporatePartnershipName { get; set; }

        public Int32? coporatePartnershipId { get; set; }

        public DateTime? membershipStartDate { get; set; }

        public decimal? discount { get; set; }

        public decimal? corporateDiscount { get; set; }

        public decimal? tax { get; set; }

        public string? voucher { get; set; }

        public string? accountingCode { get; set; }

        public string? municipalServiceCode { get; set; }

        public bool? flReceiptOnly { get; set; }

        public string? idSaleItemMigration { get; set; }

        public bool? flSwimming { get; set; }

        public bool? flAllowLocker { get; set; }

        public Int32? idMemberMembership { get; set; }

        public decimal? valueNextMonth { get; set; }

    }

    public class receivable
    {
        public Int32? idReceivable { get; set; }

        public string? description { get; set; }

        public DateTime? registrationDate { get; set; }

        public DateTime? dueDate { get; set; }

        public DateTime? receivingDate { get; set; }

        public DateTime? competenceDate { get; set; }

        public DateTime? cancellationDate { get; set; }

        public decimal? ammount { get; set; }

        public decimal? ammountPaid { get; set; }

        public status? status { get; set; }

        public Int32? currentInstallment { get; set; }

        public Int32? totalInstallments { get; set; }

        public string? authorization { get; set; }

        public string? payerName { get; set; }

        public Int32? idMemberPayer { get; set; }

        public Int32? idProspectPayer { get; set; }

        public Int32? idBranchMember { get; set; }

        public Int32? idSale { get; set; }

        public bankAccount? bankAccount { get; set; }

        public paymentType? paymentType { get; set; }

        public List<invoiceDetail>? invoiceDetails { get; set; }

        public decimal? fees { get; set; }
        public bool? conciliated { get; set; }

        public string? tid { get; set; }

        public string? nsu { get; set; }

        public DateTime? updateDate { get; set; }

        public DateTime? chargeDate { get; set; }

        public Int32? idReceivableFrom { get; set; }

        public string? cardAcquirer { get; set; }

        public string? cardFlag { get; set; }

        public string? cancellationDescription { get; set; }

        public string? source { get; set; }

        public DateTime? saleDate { get; set; }

        public logTef? logTef { get; set; }

        public List<creditDetails>? creditDetails { get; set; }
    }

    public class status
    {
        public Int32? id { get; set; }

        public string? name { get; set; }

    }

    public class bankAccount
    {
        public Int32? id { get; set; }

        public string? name { get; set; }

    }

    public class paymentType
    {
        public Int32? id { get; set; }

        public string? name { get; set; }

    }

    public class invoiceDetail
    {
        public string? invoiceNumber { get; set; }

        public decimal? issuedAmount { get; set; }

        public string? status { get; set; }

        public DateTime? sendDate { get; set; }

        public DateTime? canceledDate { get; set; }

        public string? urlPdf { get; set; }

        public Int32? idInvoiceType { get; set; }

        public string? invoiceType { get; set; }

    }

    public class logTef
    {
        public string? authorization { get; set; }
        public string? tefId { get; set; }
        public string? merchantCheckoutGuid { get; set; }

    }

    public class creditDetails
    {
        public Int32? idCredit { get; set; }

        public Int32? idCancelationCredit { get; set; }

        public Int32? idBranchOrigin { get; set; }

        public decimal? ammount { get; set; }

        public string? branchDocument { get; set; }

        public Int32? idSaleOrigin { get; set; }

        public Int32? idReceivableOrigin { get; set; }

    }


    public class filaSale  // esta es la de trabajo
    {
        public Int32? id { get; set; }
        public Int32? idSale { get; set; }
        public DateTime? saleDate { get; set; }
        public string? invoiceNumber { get; set; }
        public string? sucursal { get; set; }

        public decimal? valor { get; set; }
        public decimal? iva { get; set; }
        public decimal? ammount { get; set; }
        public decimal? ammountPaid { get; set; }

        public Int32? idMember { get; set; }
        public string? document { get; set; }
        public string? customer { get; set; }
        public string? idcustomer { get; set; }

        public Boolean VqbCliente { get; set; }
        public Boolean VqbItems { get; set; }
        public Boolean Vqbtarjetas { get; set; }
        public string? idinvoice { get; set; }
        
        public string? items { get; set; }
        public string? tarjetas { get; set; }
        public string? responsable { get; set; }
        public string? documentResponsable { get; set; }
        public string? rep { get; set; }
        public Int32? idEmployee { get; set; }

        public string? data { get; set; }
    }

    public class taxData
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? dni { get; set; }
        public string? cuit { get; set; }
        public Int32? taxType { get; set; }
    }

    public class contact
    {
        public Int32? idPhone { get; set; }
        public Int32? idMember { get; set; }
        public Int32? idEmployee { get; set; }
        public Int32? idProspect { get; set; }
        public Int32? idProvider { get; set; }
        public Int32? idContactType { get; set; }
        public string? contactType { get; set; }
        public string? description { get; set; }
    }

    public class responsible
    {
        public string? idResponsible { get; set; }
        public string? idMember { get; set; }
        public string? name { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? observation { get; set; }
        public string? idMemberResponsible { get; set; }
        public string? acessFiti { get; set; }
        public string? financialResponsible { get; set; }
    }

    public class freeze
    {
        public DateTime? startSuspend { get; set; }
        public DateTime? endSuspend { get; set; }
        public DateTime? unlockDate { get; set; }
        public Int32? idEmployee { get; set; }
        public string? reason { get; set; }
        public bool? flUseMembershipFreezeDays { get; set; }
        public Int32? daysFreeze { get; set; }
        public Int32? idFreeze { get; set; }
    }

    public class session
    {
        public Int32? idSession { get; set; }
        public DateTime? expirationDate { get; set; }
        public bool? flBonusSession { get; set; }
    }

    public class membership
    {
        public Int32? idMember { get; set; }
        public Int32? idMembership { get; set; }
        public Int32? idMemberMembership { get; set; }
        public Int32? idMemberMembershipRenewed { get; set; }
        public Int32? numMembers { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string? name { get; set; }
        public DateTime? cancelDate { get; set; }
        public string? membershipStatus { get; set; }
        public decimal? valueNextMonth { get; set; }
        public DateTime? nextCharge { get; set; }
        public Int32? idSale { get; set; }
        public DateTime? saleDate { get; set; }
        public string? contractPrinting { get; set; }
        public List<freeze>? freezes { get; set; }
        public Int32? idCategoryMembership { get; set; }
        public Int32? numberSuspensionTimes { get; set; }
        public Int32? maxSuspensionDays { get; set; }
        public Int32? minimumSuspensionDays { get; set; }
        public Int32? disponibleSuspensionDays { get; set; }
        public Int32? disponibleSuspensionTimes { get; set; }
        public Int32? daysLeftToFreeze { get; set; }
        public DateTime? loyaltyEndDate { get; set; }
        public DateTime? assessmentEndDate { get; set; }
        public bool?  flAllowLocker { get; set; }
        public bool? flAdditionalMembership { get; set; }
        public Int32? bioimpedanceAmount { get; set; }
        public bool? signedTerms { get; set; }
        public Int32? originalValue { get; set; }
        public bool? limitless { get; set; }
        public Int32? weeklyLimit { get; set; }
        public Int32? concludedSessions { get; set; }
        public Int32? pendingSessions { get; set; }
        public Int32? scheduledSessions { get; set; }
        public Int32? pendingRepositions { get; set; }
        public Int32? repositionsTotal { get; set; }
        public Int32? bonusSessions { get; set; }
        public List<session>? sessions { get; set; }
        public string? timeZone { get; set; }
        public bool? freeze { get; set; }
        public string? membershipType { get; set; }
    }

    public class member
    {
        public Int32? idMember { get; set; }
        public string? photo { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public DateTime? registerDate { get; set; }
        public Int32? idBranch { get; set; }
        public string? branchName { get; set; }
        public bool? accessBlocked { get; set; }
        public string? blockedReason { get; set; }
        public string? document { get; set; }
        public string? documentId { get; set; }
        public string? maritalStatus { get; set; }
        public string? gender { get; set; }
        public DateTime? birthDate { get; set; }
        public string? country { get; set; }
        public string? address { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? passport { get; set; }
        public string? zipCode { get; set; }
        public string? complement { get; set; }
        public string? neighborhood { get; set; }
        public string? accessCardNumber { get; set; }
        public string? number { get; set; }
        public string? idMemberMigration { get; set; }
        public taxData? taxData { get; set; }
        public string? email { get; set; }
        public string? slug { get; set; }
        public bool? penalized { get; set; }
        public string? idBranchToken { get; set; }
        public string? membershipStatus { get; set; }
        public List<contact>? contacts { get; set; }
        public DateTime? lastAccessDate { get; set; }
        public List<responsible>? responsibles { get; set; }
        public List<membership>? memberships { get; set; }
        public string? registrationKind { get; set; }
        public membership? membership { get; set; }
    }

    public class filaMember
    {
        public Int32? id { get; set; }

        public Int32? idMember { get; set; }

        public string? document { get; set; }

        public string? data { get; set; }


    }

    public class Comprobante
    {
        public Int32? idSale { get; set; }

        public Int32? idMember { get; set; }

        public Int32? idEmployee { get; set; }

        public Int32? idProspect { get; set; }

        public Int32? idEmployeeSale { get; set; }

        public DateTime? saleDate { get; set; }

        public DateTime? saleDateServer { get; set; }

        public Int32? idPersonal { get; set; }

        public string? corporatePartnershipName { get; set; }

        public Int32? coporatePartnershipId { get; set; }

        public bool? removed { get; set; }

        public Int32? idEmployeeRemoval { get; set; }

        public DateTime? removalDate { get; set; }

        public Int32? idBranch { get; set; }

        public string? observations { get; set; }

        public Int32? idSaleRecurrency { get; set; }

        public Int32? saleSource { get; set; }

        public string? idSaleMigration { get; set; }

        public List<saleItem>? saleItens { get; set; }

        public List<receivable>? receivables { get; set; }

        public member Cliente { get; set; }

    }


    public class filaUsuario
    {
        public Int32? id { get; set; }

        public Int32 idEmployee { get; set; }

        public string name { get; set; }

        public string? data { get; set; }

    }

    public class tokenEvo
    {
        public string? nombre { get; set; }

        public string? dns { get; set; }

        public string? token { get; set; }

    }


    public class filaReceivable
    {
        public Int32? id { get; set; }

        [System.ComponentModel.DisplayName("Id")]
        public Int32? idSale { get; set; }
        public Int32? idReceivable { get; set; }

        public DateTime? receivingDate { get; set; }

        public string? data { get; set; }

    }
}
