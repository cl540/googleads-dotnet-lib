// Copyright 2015, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: Anash P. Oommen

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201502;

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Util.v201502;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201502 {
  /// <summary>
  /// This code example gets all suggested ad units. To approve suggested ad
  /// units, run ApproveSuggestedAdUnits.cs. This feature is only available to
  /// DFP premium solution networks.
  ///
  /// Tags: SuggestedAdUnitService.getSuggestedAdUnitsByStatement
  /// </summary>
  class GetAllSuggestedAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all suggested ad units. To approve suggested ad units, " +
            "run ApproveSuggestedAdUnits.cs. This feature is only available to DFP premium " +
            "solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllSuggestedAdUnits();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the SuggestedAdUnitService.
      SuggestedAdUnitService suggestedAdUnitService = (SuggestedAdUnitService) user.GetService(
          DfpService.v201502.SuggestedAdUnitService);

      // Set default for page.
      SuggestedAdUnitPage page = new SuggestedAdUnitPage();

      // Create a statement to get all suggested ad units.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      try {
        do {
          // Get suggested ad units by statement.
          page = suggestedAdUnitService.getSuggestedAdUnitsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (SuggestedAdUnit suggestedAdUnit in page.results) {
              Console.WriteLine("{0}) Suggested ad unit with ID \"{1}\", and number of requests " +
                  "\"{2}\" was found.", i++, suggestedAdUnit.id, suggestedAdUnit.numRequests);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get suggested ad units. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
