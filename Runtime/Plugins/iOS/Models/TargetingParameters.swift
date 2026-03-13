//
//  TargetingParameters.swift
//  UnityFramework
//
//  Created by Dzmitry on 30/09/2024.
//

import Foundation

struct TargetingParameters {
    struct Location {
        let provider: String
        let latitude: Double
        let longitude: Double
    }

    struct ExternalUser {
        let sourceId: String
        let value: String
    }

    enum Gender: Int, Decodable {
        case female = 0
        case male
        case omitted
    }

    enum Framework: String, Decodable {
        case unity, native
    }

    let userId: String
    let gender: Gender
    let birthdayYear: Int
    let keywords: [String]
    let location: Location
    let country: String
    let city: String
    let zipCode: String
    let storeURL: String
    let storeCategory: String
    let storeSubCategories: [String]
    let framework: Framework
    let isPaid: Bool
    let externalUserIDs: [ExternalUser]
    let blockedDomains: [String]
    let blockedCategories: [String]
    let blockedApplications: [String]
}

extension TargetingParameters: Decodable {
    enum CodingKeys: String, CodingKey {
        case userId = "userId"
        case gender = "gender"
        case birthdayYear = "birthdayYear"
        case keywords = "keywords"
        case location = "deviceLocation"
        case country = "country"
        case city = "city"
        case zipCode = "zip"
        case storeURL = "storeUrl"
        case storeCategory = "storeCategory"
        case storeSubCategories = "storeSubCategories"
        case framework = "framework"
        case isPaid = "isPaid"
        case externalUserIDs = "externalUserIds"
        case blockedDomains = "blockedDomains"
        case blockedCategories = "blockedCategories"
        case blockedApplications = "blockedApplications"
    }
}

extension TargetingParameters.Location: Decodable {
    enum CodingKeys: String, CodingKey {
        case provider = "provider"
        case latitude = "latitude"
        case longitude = "longitude"
    }
}

extension TargetingParameters.ExternalUser: Decodable {
    enum CodingKeys: String, CodingKey {
        case sourceId = "sourceId"
        case value = "value"
    }
}
